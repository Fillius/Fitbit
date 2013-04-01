﻿using System;
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

namespace FitbitUploader
{
    public partial class MainForm : Form
    {
        Authorize _authorizeForm;

        DataTable _dtExercises = new DataTable("exercises");
        DataTable _dtUploadResults = new DataTable("result");
        private DataTable _dtUploaded = new DataTable();

        public string _uploadedFile = Application.StartupPath + "\\uploaded.xml";
        public string _uploadedSchema = Application.StartupPath + "\\uploaded_schema.xml";

        struct ExerciseData
        {
            public DateTime Time;
            public TimeSpan Duration;
            public string Sport;
            public string Calories;
            public string Note;
            public string Systolic;
            public string Diastolic;
            public string Pulse;
        }

        public MainForm()
        {
            InitializeComponent();

            dataGridView1.DataSource = _dtExercises;
            dataGridView2.DataSource = _dtUploadResults;

            Properties.Settings.Default.Setting = "Tested";
            Properties.Settings.Default.Save();

            if (File.Exists(_uploadedFile) && File.Exists(_uploadedSchema))
            {
                _dtUploaded.ReadXmlSchema(_uploadedSchema);
                _dtUploaded.ReadXml(_uploadedFile);
            }
        }

        public readonly oAuthFitbit _oauth = new oAuthFitbit();

        public bool AuthorizeFormAuthorizeCompleted(string oAuthVerifier)
        {
            _oauth.Verifier = oAuthVerifier;

            if (!_oauth.AccessTokenGet(_oauth.Token))
                return false;

            AppSettings.Default.AccessToken = _oauth.Token;
            AppSettings.Default.TokenSecret = _oauth.TokenSecret;
            AppSettings.Default.Verification = _oauth.Verifier;
            AppSettings.Default.Save();

            btnGetFitbitData.Enabled = true;

            return true;
        } /*AuthorizeFormAuthorizeCompleted*/


        private void btnGetFitbitData_Click(object sender, EventArgs e)
        {
            var date = dateDay.Value.ToString("yyyy-MM-dd");

            String path;

            if (rdoDataType1.Checked)
                path = "heart/date/";
            else if (rdoDataType2.Checked)
                path = "activities/date/";
            else
                return;

            APIRequest("GET", "https://api.fitbit.com/1/user/-/" + path + date + ".xml", true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if ( AppSettings.Default.AccessToken.Length > 0 &&
                 AppSettings.Default.TokenSecret.Length > 0 &&
                 AppSettings.Default.Verification.Length > 0 )
            {
                _oauth.Token = AppSettings.Default.AccessToken;
                _oauth.TokenSecret = AppSettings.Default.TokenSecret;
                _oauth.Verifier = AppSettings.Default.Verification;

                btnUploadEntry.Enabled = true;
                btnGetFitbitData.Enabled = true;
            }

            if (AppSettings.Default.PolarXMLHistoryFile.Length > 0)
                _uploadedFile = AppSettings.Default.PolarXMLHistoryFile;
        
            if (AppSettings.Default.PolarXMLHistorySchemaFile.Length > 0)
                _uploadedSchema = AppSettings.Default.PolarXMLHistorySchemaFile;

            if (AppSettings.Default.LastUpload != DateTime.MinValue)
                lblLastUploaded.Text = AppSettings.Default.LastUpload.ToString();

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

                    List<PPTExercise> exerciseList = PPTExtract.convertXmlToExercises(xml);

                    foreach (PPTExercise exercise in exerciseList)
                    {
                        _dtExercises.Rows.Add(PPTConvert.convertExerciseToDataRow(exercise, _dtExercises));
                    }

                    dataGridView1.DataSource = _dtExercises;
                    dataGridView1.AutoResizeColumns();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        } /* btnLoadPolar_Click */


        private bool APIRequest(string method, string uri, bool showResults)
        {
            if (string.IsNullOrEmpty(_oauth.Token)) _oauth.Token = AppSettings.Default.AccessToken;
            if (string.IsNullOrEmpty(_oauth.TokenSecret)) _oauth.TokenSecret = AppSettings.Default.TokenSecret;
            if (string.IsNullOrEmpty(_oauth.Verifier)) _oauth.Token = AppSettings.Default.Verification;

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

            return true;
        } /*PostRequest*/


        private void btnCreateActivity_Click(object sender, EventArgs e)
        {
            var colTime = _dtExercises.Columns.IndexOf("time") >= 0;
            var colSport = _dtExercises.Columns.IndexOf("sport") >= 0;
            var colCalories = _dtExercises.Columns.IndexOf("result_calories") >= 0;
            var colDuration = _dtExercises.Columns.IndexOf("result_duration") >= 0;
            var colNote = _dtExercises.Columns.IndexOf("note") >= 0;
            var colHrAverage = _dtExercises.Columns.IndexOf("time") >= 0;
            var colHrMax = _dtExercises.Columns.IndexOf("sport") >= 0;
            var colVo2Max = _dtExercises.Columns.IndexOf("result_calories") >= 0;

            if (!colTime || !colSport || !colCalories || !colDuration)
            {
                MessageBox.Show("No valid data to upload");
                return;
            }

            for (var row = 0; row < dataGridView1.SelectedRows.Count; row++)
            {
                var dbRow = ((DataRowView)dataGridView1.SelectedRows[row].DataBoundItem).Row;

                if (_dtExercises.Columns.Count != _dtUploaded.Columns.Count)
                    _dtUploaded = _dtExercises.Clone();

                var rowFound = false;
                var drs = _dtUploaded.Select("time like '" + dbRow["time"] + "'");

                if (drs.Length != 0)
                {
                    rowFound = true;
                    if ( MessageBox.Show(
                             "Exercise has already been uploaded, upload may cause duplicate entries",
                             "Continue?", MessageBoxButtons.YesNo ) != DialogResult.Yes )
                        continue;
                }

                var exerciseData = new ExerciseData
                    {
                        Time = Convert.ToDateTime(dataGridView1.SelectedRows[row].Cells["time"].Value),
                        Sport = dataGridView1.SelectedRows[row].Cells["sport"].Value.ToString(),
                        Calories = dataGridView1.SelectedRows[row].Cells["result_calories"].Value.ToString()
                    };

                if (colNote)
                    exerciseData.Note = dataGridView1.SelectedRows[row].Cells["note"].Value.ToString();

                TimeSpan.TryParse( dataGridView1.SelectedRows[row].Cells["result_duration"].Value.ToString(),
                                   out exerciseData.Duration );

                var uri = "https://api.fitbit.com/1/user/-/activities.xml?" +
                             "activityName=" + exerciseData.Sport +
                             "&manualCalories=" + exerciseData.Calories +
                             "&startTime=" + exerciseData.Time.ToShortTimeString() +
                             "&durationMillis=" + exerciseData.Duration.TotalMilliseconds +
                             "&date=" + exerciseData.Time.ToString("yyyy-MM-dd");

                if (!APIRequest("POST", uri, true)) continue;

                if (AppSettings.Default.LastUpload < exerciseData.Time)
                    AppSettings.Default.LastUpload = exerciseData.Time;

                if (!rowFound)
                {
                    var uploadedRow = _dtUploaded.NewRow();
                    uploadedRow.ItemArray = dbRow.ItemArray;
                    _dtUploaded.Rows.Add(uploadedRow);
                }

                if (colVo2Max)
                {
                    uri = "https://api.fitbit.com/1/user/-/heart.xml?tracker=Resting Heart Rate&heartRate=" + dataGridView1.SelectedRows[row].Cells["user-settings_vo2max"].Value
                          + "&date=" + exerciseData.Time.ToString("yyyy-MM-dd")
                          + "&time=" + exerciseData.Time.ToShortTimeString();
                    APIRequest("POST", uri, false);
                }
                if (colHrAverage)
                {
                    uri = "https://api.fitbit.com/1/user/-/heart.xml?tracker=Normal Heart Rate&heartRate=" + dataGridView1.SelectedRows[row].Cells["heart-rate_average"].Value
                          + "&date=" + exerciseData.Time.ToString("yyyy-MM-dd")
                          + "&time=" + exerciseData.Time.ToShortTimeString();
                    APIRequest("POST", uri, false);
                }
                if (colHrMax)
                {
                    uri = "https://api.fitbit.com/1/user/-/heart.xml?tracker=Exertive Heart Rate&heartRate=" + dataGridView1.SelectedRows[row].Cells["heart-rate_maximum"].Value
                          + "&date=" + exerciseData.Time.ToString("yyyy-MM-dd")
                          + "&time=" + exerciseData.Time.ToShortTimeString();
                    APIRequest("POST", uri, false);
                }
            }

            _dtUploaded.WriteXml(_uploadedFile);
            _dtUploaded.WriteXmlSchema(_uploadedSchema);
            lblLastUploaded.Text = AppSettings.Default.LastUpload.ToString();
            AppSettings.Default.Save();

        } /*btnCreateActivity_Click*/


        private void btnDelete_Click(object sender, EventArgs e)
        {
            var colHeartLogId = _dtUploadResults.Columns.IndexOf("heartLog_logId") >= 0;
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
            }
        } /*btnDelete_Click*/


        private void btnClear_Click(object sender, EventArgs e)
        {
            _dtUploadResults.Clear();
            _dtExercises.Clear();
            _dtUploadResults.Columns.Clear();
            _dtExercises.Columns.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String uri;
            var colTime = _dtExercises.Columns.IndexOf("time") >= 0;
            var colSport = _dtExercises.Columns.IndexOf("sport") >= 0;
            var colCalories = _dtExercises.Columns.IndexOf("result_calories") >= 0;
            var colDuration = _dtExercises.Columns.IndexOf("result_duration") >= 0;

            if (!colTime || !colSport || !colCalories || !colDuration)
            {
                MessageBox.Show("No valid data to upload");
                return;
            }

            for (var row = 0; row < dataGridView1.SelectedRows.Count; row++)
            {
                var exerciseData = new ExerciseData
                {
                    Time = Convert.ToDateTime(dataGridView1.SelectedRows[row].Cells["time"].Value),
                    Sport = dataGridView1.SelectedRows[row].Cells["sport"].Value.ToString(),
                    Calories = dataGridView1.SelectedRows[row].Cells["result_calories"].Value.ToString()
                };

                TimeSpan.TryParse(dataGridView1.SelectedRows[row].Cells["result_duration"].Value.ToString(),
                                   out exerciseData.Duration);

                uri = "https://api.fitbit.com/1/user/-/activities.xml?" +
                      "activityName=" + exerciseData.Sport +
                      "&manualCalories=" + exerciseData.Calories +
                      "&startTime=" + exerciseData.Time.ToShortTimeString() +
                      "&durationMillis=" + exerciseData.Duration.TotalMilliseconds +
                      "&date=" + exerciseData.Time.ToString("yyyy-MM-dd");

                APIRequest("POST", uri, true);
            }
        }

        private void btnPolar_Click(object sender, EventArgs e)
        {
            if (_authorizeForm == null || _authorizeForm.IsDisposed)
                _authorizeForm = new Authorize();

            _authorizeForm.Show();
            _authorizeForm.Navigate("https://www.polarpersonaltrainer.com/user/calendar/?startDate=11/08/2012&endDate=16/08/2012&uid=", this);
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

                 */
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
            var Settings = new frmSettings(this);
            Settings.ShowDialog(this);
        }
    }
}