using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using GenericParsing;
using System.Net;
using PolarPersonalTrainerLib;
using Fitbit.Api;
using Fitbit.Models;
using FitbitUploader.Encryption;
using FitbitUploader.Properties;
using System.Threading;

namespace FitbitUploader
{
    public partial class FrmMainForm : Form
    {
        FitbitClient fitbitClient = null;
        DataTable _dtExercises = new DataTable("exercises");
        DataTable _dtUploadResults = new DataTable("result");
        DataTable _dtFavoriteActivities = new DataTable("favorites");

        public FrmMainForm()
        {
            InitializeComponent();

            dataGridView1.DataSource = _dtExercises;
            dataGridView2.DataSource = _dtUploadResults;

            cmbFavorites.DataSource = _dtFavoriteActivities;
        }

        private void btnGetFitbitData_Click(object sender, EventArgs e)
        {
            if (rdoDataType1.Checked)
            {
                //TODO fitbitClient.getHeartLog(dateDay.Value);
            }
            else if (rdoDataType2.Checked)
            {
                var activity = fitbitClient.GetDayActivity(dateDay.Value);

                //TODO do something with the data
                MessageBox.Show("On " + dateDay.Value.ToShortDateString() + " you burned " +
                                activity.Summary.CaloriesOut.ToString() + " calories");
            }
            else
                return;

            //APIRequest("GET", "https://api.fitbit.com/1/user/-/" + path + date + ".xml", true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var consumerKey = "3e3a29f35d064a3e996ccbe8eb1f47a4";
            var consumerSecret = "b1ba6cc29e0a44768b4969ae393e241b";
            AuthCredential credentials = null;

            try
            {
                // Check if we have already received our authorized credentials
                if (string.IsNullOrWhiteSpace(AppSettings.Default.AuthToken) ||
                     string.IsNullOrWhiteSpace(AppSettings.Default.AuthTokenSecret))
                {
                    // Get the Auth credentials for the first time by directoring the
                    // user to the fitbit site to get a PIN.

                    var requestTokenUrl = "http://api.fitbit.com/oauth/request_token";
                    var accessTokenUrl = "http://api.fitbit.com/oauth/access_token";
                    var authorizeUrl = "http://www.fitbit.com/oauth/authorize";

                    var a = new Authenticator(consumerKey, consumerSecret, requestTokenUrl, accessTokenUrl, authorizeUrl);
                    var url = a.GetAuthUrlToken();

                    // Open the web browser for the user to authorize the application
                    var frmAuth = new FrmBrowser(url);
                    frmAuth.ShowDialog();

                    var pin = frmAuth.getAuthPin();

                    frmAuth.Dispose();

                    if (String.IsNullOrWhiteSpace(pin))
                    {
                        // TODO make own exception
                        throw new InvalidDataException("Did not get a valid authorisation pin");
                    }

                    credentials = a.GetAuthCredentialFromPin(pin);

                    if (credentials == null ||
                        string.IsNullOrWhiteSpace(credentials.AuthToken) ||
                        string.IsNullOrWhiteSpace(credentials.AuthTokenSecret))
                    {
                        // TODO make own exception
                        throw new InvalidDataException("Could not get authorization credentials using the authorisation pin provided");
                    }

                    SimpleAES simpleAES = new SimpleAES();

                    AppSettings.Default.AuthToken = simpleAES.EncryptToString(credentials.AuthToken);
                    AppSettings.Default.AuthTokenSecret = simpleAES.EncryptToString(credentials.AuthTokenSecret);
                    AppSettings.Default.UserId = credentials.UserId;

                    AppSettings.Default.Save();
                }
                else
                {
                    SimpleAES simpleAES = new SimpleAES();

                    credentials = new AuthCredential();
                    credentials.AuthToken = simpleAES.DecryptString(AppSettings.Default.AuthToken);
                    credentials.AuthTokenSecret = simpleAES.DecryptString(AppSettings.Default.AuthTokenSecret);
                    credentials.UserId = AppSettings.Default.UserId;
                }

                fitbitClient = new FitbitClient(consumerKey, consumerSecret, credentials.AuthToken, credentials.AuthTokenSecret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private void loadFavoriteActivities()
        {
            var favoriteActivities = fitbitClient.GetFavoriteActivities();

            if (!_dtFavoriteActivities.Columns.Contains("ActivityId"))
                _dtFavoriteActivities.Columns.Add("ActivityId");

            if (!_dtFavoriteActivities.Columns.Contains("Name"))
                _dtFavoriteActivities.Columns.Add("Name");

            foreach (ActivityReference activity in favoriteActivities.favoriteActivities)
            {

                var row = _dtFavoriteActivities.NewRow();
                row["ActivityId"] = activity.ActivityId;
                row["Name"] = activity.Name;

                _dtFavoriteActivities.Rows.Add(row);
            }

            cmbFavorites.DataSource = _dtFavoriteActivities.DefaultView;
            cmbFavorites.DisplayMember = "Name";
            cmbFavorites.BindingContext = this.BindingContext;
        }

        private void FlattenData( ref DataTable dt, XmlNodeList xmlNodes, string prefix, ref DataRow row )
        {
            foreach (XmlNode childNode in xmlNodes)
            {
                if (childNode.FirstChild == childNode.LastChild)
                {
                    if (!dt.Columns.Contains( prefix + childNode.Name))
                        dt.Columns.Add(prefix + childNode.Name);

                    row[dt.Columns.IndexOf(prefix + childNode.Name)] = childNode.InnerText;
                }
                else if (childNode.HasChildNodes)
                {
                    if (childNode.Attributes.Count > 0)
                        FlattenData(ref dt, childNode.ChildNodes, childNode.Name + childNode.Attributes[0].Value + "_", ref row);
                    else
                        FlattenData(ref dt, childNode.ChildNodes, childNode.Name + "_", ref row);
                }
            }
        } /* FlattenData */

        private void btnLoadPolar_Click(object sender, EventArgs e)
        {   
            var filePicker = new OpenFileDialog();
            filePicker.DefaultExt = ".xml";
            filePicker.Filter = "Polar XML File (*.xml)|*.xml";

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var xml = new XmlDocument();
                    
                    xml.Load(filePicker.FileName);

                    LoadPolarXml(xml);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        } /* btnLoadPolar_Click */

        private void LoadPolarXml(XmlDocument xml)
        {
            try
            {
                List<PPTExercise> exerciseList = PPTExtract.convertXmlToExercises(xml, AppSettings.Default.PPTRequireSport);

                foreach (PPTExercise exercise in exerciseList)
                {
                    _dtExercises.Rows.Add(PPTConvert.convertExerciseToDataRow(exercise, _dtExercises));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            dataGridView1.DataSource = _dtExercises;
            dataGridView1.AutoResizeColumns();
        }

        private bool APIRequest(string method, string uri, bool showResults)
        {
            /* TODO upload activity, fitbitClient.
            var response = _oauth.APIWebRequest(method, uri, null);

            if (method == "DELETE" || !showResults)
                return true;

            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            var xmlNodes = xmlDoc.GetElementsByTagName("activityLog");

            if (xmlNodes.Count <= 0)
                xmlNodes = xmlDoc.GetElementsByTagName("result");

            for (var node = xmlNodes.Count - 1; node >= 0; node--)
            {
                var logRow = _dtUploadResults.NewRow();

                FlattenData(ref _dtUploadResults, xmlNodes[node].ChildNodes, "", ref logRow);

                _dtUploadResults.Rows.Add(logRow);
            }
            dataGridView2.DataSource = _dtUploadResults;
            dataGridView2.AutoResizeColumns();
            */
            return true;
        }

        private ActivityLog convertPPTExerciseToActivityLog(PPTExercise exercise)
        {
            ActivityLog activityLog = new ActivityLog();

            activityLog.StartTime = exercise.time.ToString("HH:mm");
            activityLog.Name = exercise.sport;
            activityLog.Calories = exercise.calories;
            activityLog.Duration = Convert.ToInt32(exercise.duration.TotalMilliseconds);

            return activityLog;
        }

        private void btnCreateActivity_Click(object sender, EventArgs e)
        {
            int uploadedCount = 0;
            int skippedCount = 0;

            for (var row = 0; row < dataGridView1.SelectedRows.Count; row++)
            {
                PPTExercise exercise = PPTConvert.convertDataRowToExercise(((DataRowView)dataGridView1.SelectedRows[row].DataBoundItem).Row);

                var activityLog = convertPPTExerciseToActivityLog(exercise);

                var existingActivity = fitbitClient.GetDayActivity(exercise.time);
                bool activityFound = false;

                Thread activityDeleteThread = null;

                if (existingActivity != null)
                {
                    foreach (ActivityLog existingActivityLog in existingActivity.Activities)
                    {
                        if (existingActivityLog.Calories == activityLog.Calories &&
                            existingActivityLog.Duration == activityLog.Duration)
                        {
                            // Delete any matching excercises from MapMyFitness as they have incorrect values
                            if (existingActivityLog.Name == "Run / Jog - from MapMyFitness")
                            {
                                try
                                {
                                    if (activityDeleteThread != null)
                                        activityDeleteThread.Join();

                                    activityDeleteThread = new Thread(() => fitbitClient.DeleteActivity(existingActivityLog.LogId));
                                    activityDeleteThread.Start();
                                }
                                catch { }
                            }
                            else if (existingActivityLog.StartTime == activityLog.StartTime)
                            {
                                activityFound = true;
                            }
                        }
                    }
                }

                if (exercise.time > AppSettings.Default.LastUploadedSession)
                    AppSettings.Default.LastUploadedSession = exercise.time;

                if (activityFound)
                {
                    skippedCount++;
                    continue;
                }

                try
                {
                    Thread uploadActivityThread = new Thread(() => fitbitClient.UploadActivity(activityLog, exercise.time));
                    uploadActivityThread.Start();

                    Thread v02MaxThread = null;
                    Thread restingHRThread = null;
                    Thread averageHRThread = null;
                    Thread maxThread = null;

                    var hr = exercise.heartRate;

                    if (AppSettings.Default.UploadHR && hr != null)
                    {
                        v02MaxThread = new Thread(() => UploadHR(exercise.time, "V02Max", hr.vo2Max));
                        restingHRThread = new Thread(() => UploadHR(exercise.time, "Resting Heart Rate", hr.resting));
                        averageHRThread = new Thread(() => UploadHR(exercise.time, "Normal Heart Rate", hr.average));
                        maxThread = new Thread(() => UploadHR(exercise.time, "Exertive Heart Rate", hr.maximum));

                        v02MaxThread.Start();
                        restingHRThread.Start();
                        averageHRThread.Start();
                        maxThread.Start();
                    }

                    uploadActivityThread.Join();

                    uploadedCount++;

                    if (AppSettings.Default.UploadHR && hr != null)
                    {
                        v02MaxThread.Join();
                        restingHRThread.Join();
                        averageHRThread.Join();
                        maxThread.Join();
                    }

                    if (activityDeleteThread != null)
                        activityDeleteThread.Join();
                }
                catch (FitbitException ex)
                {
                    MessageBox.Show("HttpStatus: " + ex.HttpStatusCode.ToString() + "Upload error: " + ex.Message);
                }
            }
            MessageBox.Show(string.Format("Uploaded {0} Polar training sessions to Fitbit and skipped {1}", uploadedCount, skippedCount));
            AppSettings.Default.Save();
        }

        private void UploadHR(DateTime dateTime, string tracker, int heartRate)
        {
            HeartLog heartLog = new HeartLog();

            heartLog.Date = dateTime.ToString("yyyy-MM-dd");
            heartLog.Time = dateTime.ToString("HH:mm");
            heartLog.Tracker = tracker;
            heartLog.HeartRate = heartRate;

            try
            {
                fitbitClient.UploadHR(heartLog);
            }
            catch (FitbitException ex)
            {
                MessageBox.Show("Fitbit error uploading HR: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*var colHeartLogId = _dtUploadResults.Columns.IndexOf("heartLog_logId") >= 0;
            var colActivityLogId = _dtUploadResults.Columns.IndexOf("activityLog_logId") >= 0;
            var colLogId = _dtUploadResults.Columns.IndexOf("logId") >= 0;
            var uri = "https://api.fitbit.com";

            for (var row = 0; row < dataGridView2.SelectedRows.Count; row++)
            {
                if (colHeartLogId && !string.IsNullOrEmpty(dataGridView2.SelectedRows[row].Cells["heartLog_logId"].Value.ToString()))
                    uri += "/1/user/-/heart/" + dataGridView2.SelectedRows[row].Cells["heartLog_logId"].Value + ".xml";
                else if (colActivityLogId && !string.IsNullOrEmpty(dataGridView2.SelectedRows[row].Cells["activityLog_logId"].Value.ToString()))
                    uri += "/1/user/-/activities/" + dataGridView2.SelectedRows[row].Cells["activityLog_logId"].Value + ".xml";
                else if (colLogId && !string.IsNullOrEmpty(dataGridView2.SelectedRows[row].Cells["logId"].Value.ToString()))
                    uri += "/1/user/-/activities/" + dataGridView2.SelectedRows[row].Cells["logId"].Value + ".xml";
                else
                    return;

                APIRequest("DELETE", uri, false);

                dataGridView2.Rows.RemoveAt(row);

                row--;
            }*/
        } /* btnDelete_Click */

        private void btnClear_Click(object sender, EventArgs e)
        {
            _dtUploadResults.Clear();
            _dtExercises.Clear();
            _dtUploadResults.Columns.Clear();
            _dtExercises.Columns.Clear();
        }

        private void ReadCsvData( DataTable dt, ref DataRow row, GenericParser csvParser, string col)
        {
            if (!dt.Columns.Contains(col))
                dt.Columns.Add(col);

            row[dt.Columns.IndexOf(col)] = csvParser[col];
        }

        private void btnBPLoad_Click(object sender, EventArgs e)
        {
            var filePicker = new OpenFileDialog();
            filePicker.DefaultExt = ".csv";
            filePicker.Filter = "Braun TrueScan CSV File (*.csv)|*.csv";

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var csvParser = new GenericParser();
                    csvParser.ColumnDelimiter = ';';
                    csvParser.SetDataSource(filePicker.FileName);

                    MessageBox.Show("done");

                    csvParser.FirstRowHasHeader = true;

                    while (csvParser.Read())
                    {
                        var exerciseData = _dtExercises.NewRow();

                        ReadCsvData(_dtExercises, ref exerciseData, csvParser, "Systolic");
                        ReadCsvData(_dtExercises, ref exerciseData, csvParser, "Diastolic");
                        ReadCsvData(_dtExercises, ref exerciseData, csvParser, "Pulse");
                        ReadCsvData(_dtExercises, ref exerciseData, csvParser, "Date/Time");

                        _dtExercises.Rows.Add(exerciseData);
                    }

                    dataGridView1.DataSource = _dtExercises;
                    dataGridView1.AutoResizeColumns();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnBPUpload_Click(object sender, EventArgs e)
        {
            /* TODO
            var colSystolic = _dtExercises.Columns.IndexOf("Systolic") >= 0;
            var colDiastolic = _dtExercises.Columns.IndexOf("Diastolic") >= 0;
            var colPulse = _dtExercises.Columns.IndexOf("Pulse") >= 0;
            var colDateTime = _dtExercises.Columns.IndexOf("Date/Time") >= 0;

            if ( !colSystolic || !colDiastolic || !colPulse || !colDateTime )
            {
                MessageBox.Show("No valid data to upload");
                return;
            }

            for (var row = 0; row < dataGridView1.SelectedRows.Count; row++)
            {
                /*var dbRow = ((DataRowView)dataGridView1.SelectedRows[row].DataBoundItem).Row;

                if (_dtExercises.Columns.Count != _dtUploaded.Columns.Count)
                    _dtUploaded = _dtExercises.Clone();

                var rowFound = false;
                var drs = _dtUploaded.Select("time like '" + dbRow["time"] + "'");

                if (drs.Length != 0)
                {
                    rowFound = true;
                    if (MessageBox.Show(
                             "Exercise has already been uploaded, upload may cause duplicate entries",
                             "Continue?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        continue;
                }*/

                /* TODO
                var exerciseData = new
                    ExerciseData
                    {
                        Systolic = dataGridView1.SelectedRows[row].Cells["Systolic"].Value.ToString(),
                        Diastolic =
                            dataGridView1.SelectedRows[row].Cells["Diastolic"].Value.ToString(),
                        Pulse = dataGridView1.SelectedRows[row].Cells["Pulse"].Value.ToString(),
                        Time = Convert.ToDateTime(dataGridView1.SelectedRows[row].Cells["Date/Time"].Value)
                    };

                var uri = "https://api.fitbit.com/1/user/-/bp.xml?" +
                          "systolic=" + exerciseData.Systolic +
                          "&diastolic=" + exerciseData.Diastolic +
                          "&date=" + exerciseData.Time.ToString("yyyy-MM-dd") +
                          "&time=" + exerciseData.Time.ToShortTimeString();

                if (!APIRequest("POST", uri, true)) continue;

                /*if (AppSettings.Default.LastUpload < exerciseData.Time)
                    AppSettings.Default.LastUpload = exerciseData.Time;

                if (!rowFound)
                {
                    var uploadedRow = _dtUploaded.NewRow();
                    uploadedRow.ItemArray = dbRow.ItemArray;
                    _dtUploaded.Rows.Add(uploadedRow);
                }

                 *
                if (colPulse)
                {
                    uri = "https://api.fitbit.com/1/user/-/heart.xml?tracker=BP Measurement&heartRate=" +
                          exerciseData.Pulse
                          + "&date=" + exerciseData.Time.ToString("yyyy-MM-dd")
                          + "&time=" + exerciseData.Time.ToShortTimeString();
                    APIRequest("POST", uri, false);
                }
            }

            /*_dtUploaded.WriteXml(_uploadedFile);
            _dtUploaded.WriteXmlSchema(_uploadedSchema);
            lblLastUploaded.Text = AppSettings.Default.LastUpload.ToString();
            AppSettings.Default.Save();*/
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var Settings = new FrmSettings();
            Settings.ShowDialog(this);
        }

        private void btnGetPolarSessions_Click(object sender, EventArgs e)
        {
            if (AppSettings.Default.PPTUser.Length == 0 ||
                 AppSettings.Default.PPTPassword.Length == 0)
            {
                MessageBox.Show("To use this function, enter your PolarPersonalTrainer.com username and password in the settings menu");
                return;
            }

            SimpleAES simpleAES = new SimpleAES();

            try
            {
                PPTExport exporter = new PPTExport(
                    simpleAES.DecryptString(AppSettings.Default.PPTUser),
                    simpleAES.DecryptString(AppSettings.Default.PPTPassword));
                
                var startDate = AppSettings.Default.LastUploadedSession;

                var xml = exporter.downloadSessions(startDate, DateTime.Today);

                if (xml == null)
                    throw new PPTException("No valid training sessions returned");

                LoadPolarXml(xml);
            }
            catch (PPTException ex)
            {
                MessageBox.Show("PolarPersonalTrainer error: " + ex.Message);
            }
        }

        private void btnChangeSport_Click(object sender, EventArgs e)
        {

        }

        private void cmbFavorites_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFavorites_DropDown(object sender, EventArgs e)
        {
            if (_dtFavoriteActivities.Rows.Count == 0)
                loadFavoriteActivities();
        }
    }
}
