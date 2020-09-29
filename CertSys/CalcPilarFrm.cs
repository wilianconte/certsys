using CerSys.Data.Model;
using CerSys.Domain;
using System;
using System.Windows.Forms;

namespace CertSys
{
    public partial class CalcPilarFrm : Form
    {

        #region Properts
        private readonly ICalcServices _calcServices;
        #endregion

        public CalcPilarFrm(ICalcServices calcServices)
        {
            _calcServices = calcServices;

            InitializeComponent();
        }

        //private void btnCalc_Click(object sender, EventArgs e)
        //{
        //    if (!validar())            
        //        return;

        //    double reta = Convert.ToDouble(txtReta.Text);
        //    double vao = Convert.ToDouble(txtVao.Text);
        //    double reforco = Convert.ToDouble(txtBase.Text);

        //    var project = _calcServices.Project(reta, vao, reforco);
        //    ProjectCalculate = _calcServices.CalculateProject(project);
            
        //    txtResult.Text = ProjectCalculate.ToString();

        //    btnSave.Enabled = true;
        //}

        //private bool validar()
        //{
        //    if (string.IsNullOrEmpty(txtReta.Text) || string.IsNullOrEmpty(txtVao.Text) || string.IsNullOrEmpty(txtVao.Text))
        //    {
                
        //        MessageBox.Show("Preencha todos os dados antes de prossguir.", "Erro!", MessageBoxButtons.OK);

        //        return false;
        //    }

        //    if (Convert.ToDouble(txtVao.Text) < 2)
        //    {
        //        MessageBox.Show("Vão deve ter no mínimo 2 metros.", "Erro!", MessageBoxButtons.OK);

        //        return false;
        //    }

        //    return true;
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            var result = _calcServices.SaveConfiguration(new Configuration 
            { 
                MaxVao = Convert.ToDouble(txtVao.Text),
                MinTotal = Convert.ToDouble(txtReta.Text),
                MaxBaseReforcada = Convert.ToDouble(txtBase.Text)
            });

            if (result > 0)
            {
                MessageBox.Show("Configuração salva com sucesso!", "Informação!", MessageBoxButtons.OK);
            }
        }

    }
}
