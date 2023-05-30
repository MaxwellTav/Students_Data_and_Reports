using Students_Data_and_Reports.Properties;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Students_Data_and_Reports
{
    public partial class MainForm : Form
    {
        #region El constructor
        public MainForm()
        { InitializeComponent(); }
        #endregion

        #region Variables
        //Título del programa.
        string programTitle = "REGISTRO DE ESTUDIANTES";

        //Cadena de conexión (No asignar ningún valor, dejar en blanco).
        string connectionString;

        string ubicacinFoto;
        DataGridViewRow currentRow = new DataGridViewRow();

        controlEstado controlEstado = controlEstado.Nada;
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            studentPicture.ImageLocation = Application.StartupPath + "/Fotos/placeholderPhoto.jpeg";
            #region Login implementation (deprecated)
            //LoginForm loginForm = new LoginForm();

            ////La primera vez.
            //if (Settings.Default.firstTime)
            //{
            //    Settings.Default .firstTime = false;

            //    loginForm.firstTime = true;

            //    loginForm.messageLabel.Text = "Ingrese su numera contraseña\nEs obligatorio";

            //    Settings.Default.Save();
            //    return;
            //}

            //loginForm.firstTime = false;
            //loginForm.messageLabel.Text = "Ingrese su contraseña";
            //loginForm.ShowDialog();

            //if (loginForm.dialogResult == DialogResult.OK)
            //{
            //    //Cargar cadena de conexión.
            //    connectionString = Settings.Default.connectionString;
            //}
            //else
            //{
            //    Close();
            //    Dispose();
            //}
            #endregion

            //Obtener la connectionString
            connectionString = Settings.Default.connectionString;

            Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            Location = new System.Drawing.Point(0, 0);

            CargarDatosAsync(GetQueryStudents(null, false), MaindataGridView);
            enableControls(false);
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            Text = $@"  {programTitle}       |       {DateTime.Now.ToString("hh:mm:ss tt       |       dddd - dd/MMMM/yyyy")}";
        }

        private async void CargarDatosAsync(string query, DataGridView dgv)
        {
            try
            {
                // Código para establecer la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync(); // Abrir la conexión de forma asíncrona

                    // Crear un objeto SqlCommand para ejecutar una consulta
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync(); // Ejecutar la consulta y leer los datos de forma asíncrona

                        // Limpiar el DataGridView antes de cargar los nuevos datos
                        dgv.Rows.Clear();
                        dgv.Columns.Clear();

                        // Agregar columnas al DataGridView según las columnas de la consulta
                        for (int i = 0; i < reader.FieldCount; i++)
                            dgv.Columns.Add(reader.GetName(i), reader.GetName(i));

                        // Leer los datos y agregar filas al DataGridView
                        while (await reader.ReadAsync()) // Leer cada fila de forma asíncrona
                        {
                            object[] rowData = new object[reader.FieldCount];
                            reader.GetValues(rowData);
                            dgv.Rows.Add(rowData);
                        }

                        reader.Close(); // Cerrar el lector de datos
                        connection.Close(); // Cerrar la conexión
                    }
                }
            }
            catch (Exception error)
            { MessageBox.Show("No se pudo conectar con la base de datos\n\n" + error.Message, "¡Error de conexión " + error.HResult + "!", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            finally { enableControls(false); controlEstado = controlEstado.Nada; }
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.firstTime = true;
            loginForm.messageLabel.Text = "Ingrese su contraseña";

            loginForm.ShowDialog();

            Settings.Default.password = loginForm.messageLabel.Text;
            Settings.Default.Save();
        }

        private void resetearCambiosDelProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.firstTime = true;
            Settings.Default.Save();
        }

        private void cadenaDeConexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionStringForm connectionStringForm = new ConnectionStringForm();
            connectionStringForm.ShowDialog();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controlEstado != controlEstado.Nada)
            {
                MessageBox.Show("Usted ya está haciendo un proceso, termine de " + controlEstado + " para poder hacer esta acción.", "¡Error de procesos!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            SeachForm seachForm = new SeachForm();
            seachForm.ShowDialog();

            CargarDatosAsync(GetQueryStudents(seachForm.OutputQuery, true), MaindataGridView);
        }

        /// <summary>
        /// Salida del query.
        /// </summary>
        /// <param name="input">La entrada que recibió para buscar.</param>
        /// <param name="busqueda">Boleano que indica sí se está buscando</param>
        /// <returns></returns>
        string GetQueryStudents(string input, bool busqueda)
        {
            string _query = $@"Select * From DatosPersonales";

            if (busqueda)
            {
                _query += $@" 
                                Where Nombre Like '%{input}%' Or 
                                Apellido Like '%{input}%' Or 
                                Cedula Like '%{input}%' Or 
                                Telefono Like '%{input}%' Or 
                                Correo Like '%{input}%' Or 
                                EstadoCivil Like '%{input}%' Or 
                                FechaNatal Like '%{input}%' Or 
                                Nacionalidad Like '%{input}%' Or 
                                Dirección Like '%{input}%' Or 
                                ComunidadIndegena Like '%{input}%' Or 
                                PaisResidencia Like '%{input}%' Or 
                                Estado Like '%{input}%' Or 
                                Ciudad Like '%{input}%' Or 
                                Creencias Like '%{input}%' Or 
                                GradoInstruccion Like '%{input}%' Or 
                                OtraCarrera Like '%{input}%' Or 
                                CarreraCursante Like '%{input}%' Or 
                                SemetreCursante Like '%{input}%' Or 
                                ModalidadEstudio Like '%{input}%' Or 
                                AsociacionAdventista Like '%{input}%'";
            }

            return _query;
        }

        private void MaindataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (controlEstado != controlEstado.Nada)
            {
                MessageBox.Show("Usted ya está haciendo un proceso, termine de " + controlEstado + " para poder hacer esta acción.", "¡Error de procesos!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = MaindataGridView.Rows[e.RowIndex];

                if (e.RowIndex >= 0 && e.RowIndex < MaindataGridView.Rows.Count)
                {
                    if (selectedRow.Cells.Count > 0)
                    {
                        currentRow = selectedRow;

                        //ID.
                        textBox1.Text = selectedRow.Cells[1].Value.ToString();

                        //Nombre.
                        textBox2.Text = selectedRow.Cells[2].Value.ToString();

                        //Apellido.
                        textBox3.Text = selectedRow.Cells[3].Value.ToString();

                        //Sexo.
                        comboBox1.Text = selectedRow.Cells[4].Value.ToString();

                        //Estado Civil.
                        comboBox2.Text = selectedRow.Cells[7].Value.ToString();

                        //Teléfono.
                        textBox5.Text = selectedRow.Cells[8].Value.ToString();

                        //Fecha nac.
                        textBox4.Text = selectedRow.Cells[10].Value.ToString();

                        //Nacionalidad.
                        comboBox3.Text = selectedRow.Cells[14].Value.ToString();

                        //Dirección de residencia.
                        textBox6.Text = selectedRow.Cells[12].Value.ToString();

                        //Posee otra carrera.
                        comboBox4.Text = selectedRow.Cells[19].Value.ToString();

                        //Carrera actual.
                        comboBox5.Text = selectedRow.Cells[20].Value.ToString();

                        //Semestre actual.
                        comboBox6.Text = selectedRow.Cells[21].Value.ToString();

                        //Turno.
                        comboBox7.Text = selectedRow.Cells[22].Value.ToString();

                        //Residencia.
                        comboBox8.Text = selectedRow.Cells[23].Value.ToString();

                        //Modalidad.
                        comboBox9.Text = selectedRow.Cells[24].Value.ToString();

                        //Creencia
                        comboBox10.Text = selectedRow.Cells[17].Value.ToString();

                        //Ciudad
                        textBox7.Text = selectedRow.Cells[16].Value.ToString();

                        //Correo
                        textBox8.Text = selectedRow.Cells[5].Value.ToString();

                        //Pais.
                        textBox9.Text = selectedRow.Cells[14].Value.ToString();

                        //Grado
                        comboBox11.Text = selectedRow.Cells[18].Value.ToString();

                        //Estado
                        comboBox12.Text = selectedRow.Cells[15].Value.ToString();

                        //Redes Sociales
                        comboBox14.Text = selectedRow.Cells[9].Value.ToString();

                        //Asociación Indigenas
                        comboBox15.Text = selectedRow.Cells[13].Value.ToString();

                        //Asociación Adventista
                        comboBox13.Text = selectedRow.Cells[25].Value.ToString();

                        //Foto
                        studentPicture.ImageLocation = selectedRow.Cells[6].Value.ToString();
                    }
                }
                MessageBox.Show("Se ha seleccionado un estudiante.", "¡Dato encontrado!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            catch (Exception error)
            {
                { MessageBox.Show("Hubieron uno o varios campos que no se pudieron traer\n\nRazón:\n" + error.Message, "¡Error de datos " + error.HResult + "!", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (controlEstado != controlEstado.Nada)
            {
                MessageBox.Show("Usted ya está haciendo un proceso, termine de " + controlEstado + " para poder hacer esta acción.", "¡Error de procesos!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            CargarDatosAsync(GetQueryStudents(null, false), MaindataGridView);
            limpiarCampos();
            MessageBox.Show("Se han actualizado los datos.", "¡Hecho!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controlEstado != controlEstado.Nada)
            {
                MessageBox.Show("Usted ya está haciendo un proceso, termine de " + controlEstado + " para poder hacer esta acción.", "¡Error de procesos!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            MessageBox.Show("Rellene los campos y presione \"Guardar\" para guardar los datos.", "Siga las instrucciones.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            limpiarCampos();
            enableControls(true);
            controlEstado = controlEstado.crear;
        }

        void limpiarCampos()
        {
            //Limpiar los campos.
            foreach (Control control in StudentGroupBox.Controls)
            {
                if (control.GetType() == typeof(TextBox) || control.GetType() == typeof(ComboBox))
                {
                    control.Enabled = true;
                    control.Text = string.Empty;
                }
            }

            FotoButton.Image = null;
            studentPicture.ImageLocation = null;
            currentRow = null;
            ubicacinFoto = string.Empty;
        }

        void enableControls(bool on)
        {
            foreach (Control control in StudentGroupBox.Controls)
            {
                if (control.GetType() == typeof(TextBox) || control.GetType() == typeof(ComboBox))
                {
                    if (on)
                    {
                        FotoButton.Visible = true;
                        FotoButton.Enabled = true;

                        control.Enabled = true;

                        SaveButton.Enabled = true;
                        SaveButton.Visible = true;
                    }
                    else
                    {
                        FotoButton.Visible = false;
                        FotoButton.Enabled = false;

                        control.Enabled = false;

                        SaveButton.Enabled = false;
                        SaveButton.Visible = false;
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "";

                    //Sí estoy creando
                    if (controlEstado == controlEstado.crear)
                    {
                        query = $@"
                        Update DatosPersonales
                        VALUES (
                                --Cédula
                            '{textBox1.Text}',
                                --Nombre
                            '{textBox2.Text}',
                                --Apellido
                            '{textBox3.Text}',
                                --Sexo
                            '{comboBox1.Text}',
                                --Correo
                            '{textBox8.Text}',
                                --Foto
                            '{ubicacinFoto}',
                                --Estado Civil
                            '{comboBox2.Text}',
                                --Teléfono
                            '{textBox5.Text}',
                                --Redes Sociales
                            '{comboBox14.Text}',
                                --Fecha de nacimiento
                            '{textBox4.Text}',
                                --Nacionalidad
                            '{comboBox3.Text}',
                                --Dir. Residencia
                            '{textBox6.Text}',
                                --Asociación indígena
                            '{comboBox15.Text}',
                                --País de residencia
                            '{textBox9.Text}',
                                --Estado
                            '{comboBox12.Text}',
                                --Ciudad
                            '{textBox7.Text}',
                                --Creencia
                            '{comboBox10.Text}',
                                --Grado
                            '{comboBox11.Text}',
                                --Posee otra carrera
                            '{comboBox4.Text}',
                                --Carrera Actual
                            '{comboBox5.Text}',
                                --Semetre actual
                            {comboBox6.Text},
                                --Turno actual
                            '{comboBox7.Text}',
                                --Residencia
                            '{comboBox8.Text}',
                                --Modalidad
                            '{comboBox9.Text}',
                                --Asociación
                            '{comboBox13.Text}'
                        );";
                    }
                    //Estoy editando.
                    else if (controlEstado == controlEstado.editar)
                    {
                        query = $@"UPDATE [DatosPersonales]
                                       SET [Cedula] = '{textBox1.Text}'
                                          ,[Nombre] = '{textBox2.Text}'
                                          ,[Apellido] = '{textBox3.Text}'
                                          ,[Sexo] = '{comboBox1.Text}'
                                          ,[Correo] = '{textBox8.Text}'
                                          ,[Foto] = '{ubicacinFoto}'
                                          ,[EstadoCivil] = '{comboBox2.Text}'
                                          ,[Telefono] = '{textBox5.Text}'
                                          ,[RedesSociales] = '{comboBox14.Text}'
                                          ,[FechaNatal] = '{textBox4.Text}'
                                          ,[Nacionalidad] = '{comboBox3.Text}'
                                          ,[Dirección] = '{textBox6.Text}'
                                          ,[ComunidadIndegena] = '{comboBox15.Text}'
                                          ,[PaisResidencia] = '{textBox9.Text}'
                                          ,[Estado] = '{comboBox12.Text}'
                                          ,[Ciudad] = '{textBox7.Text}'
                                          ,[Creencias] = '{comboBox10.Text}'
                                          ,[GradoInstruccion] = '{comboBox11.Text}'
                                          ,[OtraCarrera] = '{comboBox4.Text}'
                                          ,[CarreraCursante] = '{comboBox5.Text}'
                                          ,[SemetreCursante] = '{comboBox6.Text}'
                                          ,[TurnoEnQueEstudia] = '{comboBox7.Text}'
                                          ,[Residencia] = '{comboBox8.Text}'
                                          ,[ModalidadEstudio] = '{comboBox9.Text}'
                                          ,[AsociacionAdventista] = '{comboBox13.Text}'
                                     WHERE IDEstudiante = '{currentRow.Cells[0].Value.ToString()}'";
                    }
                    else
                    {
                        enableControls(false);
                        limpiarCampos();
                        return;
                    }

                    int studenID = MaindataGridView.Rows.Count + 1;
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    CargarDatosAsync(GetQueryStudents(null, false), MaindataGridView);
                    limpiarCampos();
                    enableControls(false);
                    MessageBox.Show("Se han actualizado los datos.", "¡Hecho!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Hubieron uno o varios campos que no se pudieron guardar\n\nRazón:\n" + error.Message, "¡Error de datos " + error.HResult + "!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Clipboard.SetText(error.Message);
            }
        }

        private void FotoButton_Click(object sender, EventArgs e)
        {
            // Crear una instancia del OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Establecer las propiedades del cuadro de diálogo
            openFileDialog.Title = "Seleccionar foto";
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo y verificar si el usuario hizo clic en el botón "Aceptar"
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Obtener la ubicación del archivo seleccionado
                string ubicacionFoto = openFileDialog.FileName;
                ubicacinFoto = ubicacionFoto;

                // Cargar la imagen en el PictureBox
                studentPicture.Image = Image.FromFile(ubicacionFoto);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controlEstado != controlEstado.Nada)
            {
                MessageBox.Show("Usted ya está haciendo un proceso, termine de " + controlEstado + " para poder hacer esta acción.", "¡Error de procesos!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $@"Delete From DatosPersonales Where IDEstudiante = '{currentRow.Cells[0].Value}'";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    CargarDatosAsync(GetQueryStudents(null, false), MaindataGridView);
                    limpiarCampos();
                    enableControls(false);
                    MessageBox.Show("Se eliminado el dato.", "¡Hecho!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception error)
            {
                switch (error.HResult)
                {
                    case -2146233086:
                        MessageBox.Show("Hubieron uno o varios campos que no se pudieron eliminar\n\nRazón:\nDebe seleccionar a un estudiante, solo uno.", "¡Error de datos " + error.HResult + "!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;

                    default:
                        MessageBox.Show("Hubieron uno o varios campos que no se pudieron eliminar\n\nRazón:\n" + error.Message, "¡Error de datos " + error.HResult + "!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                }
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controlEstado != controlEstado.Nada)
            {
                MessageBox.Show("Usted ya está haciendo un proceso, termine de " + controlEstado + " para poder hacer esta acción.", "¡Error de procesos!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            controlEstado = controlEstado.editar;
            try
            {
                string fvalue = currentRow.Cells[0].Value.ToString();
                ubicacinFoto = currentRow.Cells[6].Value.ToString();

                MessageBox.Show("Rellene los campos y presione \"Guardar\" para guardar los datos.", "Siga las instrucciones.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                enableControls(true);
            }
            catch
            {
                MessageBox.Show("Hubieron uno o varios campos que no se pudieron editar\n\nRazón:\nDebe seleccionar a un estudiante, solo uno.", "¡Error de datos!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            enableControls(false);

            MessageBox.Show("Se canceló la acción: " + controlEstado + ".", "¡Liberación de procesos!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            controlEstado = controlEstado.Nada;

        }
    }

    public enum controlEstado
    {
        Nada,
        editar,
        crear,
        eliminando
    }
}
