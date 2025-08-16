﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS.OOBE
{
    public partial class PCWelcomeBG : Form
    {
        public PCWelcomeBG()
        {
            InitializeComponent();
            MostrarWelcome();
        }

        private void CargarPantalla(Form pantalla)
        {
            panelContenedor.Controls.Clear();
            pantalla.TopLevel = false;
            pantalla.FormBorderStyle = FormBorderStyle.None;
            // Ubicación en medio del formulario
            pantalla.Location = new Point(
                (panelContenedor.Width - pantalla.Width) / 2,
                (panelContenedor.Height - pantalla.Height) / 2
            );
            panelContenedor.Controls.Add(pantalla);
            pantalla.Show();
        }

        private void MostrarWelcome()
        {
            var welcome = new PCWelcome();
            welcome.PantallaTerminada += (s, e) => MostrarLanguage(); // Evento que dispara el hijo
            CargarPantalla(welcome);
        }

        private void MostrarLanguage()
        {
            var lang = new PCLanguage();
            lang.PantallaTerminada += (s, e) => MostrarRegion();
            CargarPantalla(lang);
        }

        private void MostrarRegion()
        {
            var region = new PCRegion();
            region.PantallaTerminada += (s, e) => MostrarPrivacy();
            CargarPantalla(region);
        }
        private void MostrarPrivacy()
        {
            var privacy = new PCPrivacy();
            privacy.PantallaTerminada += (s, e) => MostrarAccount();
            CargarPantalla(privacy);
        }
        private void MostrarAccount()
        {
            var account = new PCAccount();
            account.PantallaTerminada += (s, e) => 
            {
                MessageBox.Show((string)T("usrcrscc"), (string)T("suc"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarFinish(); 
            };
            CargarPantalla(account);
        }
        private void MostrarFinish()
        {
            var finish = new PCFinish();
            finish.PantallaTerminada += (s, e) => CerrarOOBE();
            CargarPantalla(finish);
        }
        private void CerrarOOBE()
        {
            Application.Restart();
        }
    }
}
