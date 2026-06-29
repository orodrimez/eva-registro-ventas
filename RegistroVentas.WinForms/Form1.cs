using RegistroVentas.WinForms.Models;
using RegistroVentas.WinForms.Services;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace RegistroVentas.WinForms
{
    public partial class Form1 : Form
    {
        private readonly RegistroVentasApiClient _api;
        private readonly JavaScriptSerializer _serializer;

        private string _token;

        public Form1()
        {
            InitializeComponent();

            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            _api = new RegistroVentasApiClient(baseUrl);
            _serializer = new JavaScriptSerializer();

            cmbRol.Items.Clear();
            cmbRol.Items.Add("Operador");
            cmbRol.Items.Add("Supervisor");
            //cmbRol.Items.Add("Auditor");
            cmbRol.SelectedIndex = 0;

            txtUsuario.Text = "";
            txtImporte.Text = "";
            txtPage.Text = "1";
            txtPageSize.Text = "10";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void btnGenerarToken_Click(object sender, EventArgs e)
        {
            await ExecuteAsync(async () =>
            {
                var usuario = txtUsuario.Text.Trim();
                var rol = cmbRol.Text.Trim();

                if (string.IsNullOrWhiteSpace(usuario))
                {
                    MessageBox.Show("Captura el usuario.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(rol))
                {
                    MessageBox.Show("Selecciona un rol.");
                    return;
                }

                var response = await _api.GenerarTokenAsync(usuario, rol);

                _token = response.accessToken;

                Mostrar(new
                {
                    mensaje = "Token generado",
                    rol = rol,
                    tokenType = response.tokenType,
                    expiresInSeconds = response.expiresInSeconds,
                    accessToken = response.accessToken
                });
            });
        }

        private async void btnCifrarSecreto_Click(object sender, EventArgs e)
        {
            await ExecuteAsync(async () =>
            {
                var secretoPlano = txtSecretoPlano.Text.Trim();

                if (string.IsNullOrWhiteSpace(secretoPlano))
                {
                    MessageBox.Show("Captura el secreto en texto plano.");
                    return;
                }

                var secretoCifrado = await _api.CifrarSecretoAsync(secretoPlano);

                txtSecretoCifrado.Text = secretoCifrado;

                Mostrar(new
                {
                    mensaje = "Secreto cifrado con AES-256",
                    secreto = secretoCifrado
                });
            });
        }

        private async void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            await ExecuteAsync(async () =>
            {
                ValidarToken();

                var cliente = txtCliente.Text.Trim();
                var importe = txtImporte.Text.Trim();
                var secretoCifrado = txtSecretoCifrado.Text.Trim();

                if (string.IsNullOrWhiteSpace(cliente))
                {
                    MessageBox.Show("Captura el cliente.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(importe))
                {
                    MessageBox.Show("Captura el importe.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(secretoCifrado))
                {
                    MessageBox.Show("Primero cifra el secreto.");
                    return;
                }

                var response = await _api.RegistrarVentaAsync(
                    _token,
                    cliente,
                    importe,
                    secretoCifrado
                );

                txtReferenciaCancelacion.Text = response.referencia;
                txtPk.Text = response.pk.ToString();

                Mostrar(response);
            });
        }

        private async void btnConsultarPk_Click(object sender, EventArgs e)
        {
            await ExecuteAsync(async () =>
            {
                ValidarToken();

                int pk;

                if (!int.TryParse(txtPk.Text.Trim(), out pk) || pk <= 0)
                {
                    MessageBox.Show("Captura un PK válido.");
                    return;
                }

                var response = await _api.ObtenerPorPkAsync(_token, pk);

                Mostrar(response);
            });
        }

        private async void btnConsultarPaginado_Click(object sender, EventArgs e)
        {
            await ExecuteAsync(async () =>
            {
                ValidarToken();

                int page;
                int pageSize;

                if (!int.TryParse(txtPage.Text.Trim(), out page))
                {
                    MessageBox.Show("page inválido.");
                    return;
                }

                if (!int.TryParse(txtPageSize.Text.Trim(), out pageSize))
                {
                    MessageBox.Show("pageSize inválido.");
                    return;
                }

                var response = await _api.ObtenerPaginadoAsync(
                    _token,
                    page,
                    pageSize
                );

                Mostrar(response);
            });
        }

        private async void btnCancelar_Click(object sender, EventArgs e)
        {
            await ExecuteAsync(async () =>
            {
                ValidarToken();

                var referencia = txtReferenciaCancelacion.Text.Trim();

                if (string.IsNullOrWhiteSpace(referencia))
                {
                    MessageBox.Show("Captura la referencia.");
                    return;
                }

                var response = await _api.CancelarAsync(_token, referencia);

                Mostrar(response);
            });
        }

        private void ValidarToken()
        {
            if (string.IsNullOrWhiteSpace(_token))
            {
                throw new InvalidOperationException(
                    "Genera un token."
                );
            }
        }

        private async Task ExecuteAsync(Func<Task> action)
        {
            try
            {
                ToggleButtons(false);

                await action();
            }
            catch (ApiException ex)
            {
                txtResultado.Text =
                    "ERROR API" + Environment.NewLine +
                    "HTTP: " + ex.StatusCode + Environment.NewLine +
                    "Status: " + ex.Status + Environment.NewLine +
                    "Body:" + Environment.NewLine +
                    ex.Body;
            }
            catch (Exception ex)
            {
                txtResultado.Text =
                    "ERROR" + Environment.NewLine +
                    ex.Message;
            }
            finally
            {
                ToggleButtons(true);
            }
        }

        private void Mostrar(object value)
        {
            txtResultado.Text = _serializer.Serialize(value);
        }

        private void ToggleButtons(bool enabled)
        {
            btnGenerarToken.Enabled = enabled;
            btnCifrarSecreto.Enabled = enabled;
            btnRegistrarVenta.Enabled = enabled;
            btnConsultarPk.Enabled = enabled;
            btnConsultarPaginado.Enabled = enabled;
            btnCancelar.Enabled = enabled;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}