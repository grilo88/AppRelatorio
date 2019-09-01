using AppRelatorio.Banco;
using Microsoft.Data.Sqlite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRelatorio
{
    public partial class App : Application
    {
        public App()
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("pt-br");
            string licenseKey = "MTM0MjQyQDMxMzcyZTMyMmUzMEdRQ01xc2ZVVTI2QVAwYzB0b1ZIV3QrTytBekkxd0YxY2d0bjd2MVd0Yzg9;MTM0MjQzQDMxMzcyZTMyMmUzMGZkNXFWaE9qb2pXdXpGMHhBZyt6dW9BK1drK2oxWEtDKzVLcE4xZmlrQ289;MTM0MjQ0QDMxMzcyZTMyMmUzMEUxTjFtZ3VuV1Bvd095d1ZQdFFma0xPaHVyZU1FVE5sWDNUdHZTdm5EbTg9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);

            InitializeComponent();

            if (!Model.Publicador.AlgumRegistro())
            {
                using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
                {
                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Maike",
                        Sobrenome = "Apolinario",
                        Email = "maike@hotmail.com",
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "João",
                        Sobrenome = "Alves",
                        Email = "joao@hotmail.com",
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Rodrigo",
                        Sobrenome = "Vieira Santana",
                        Email = "rodrigo@hotmail.com",
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Guilherme",
                        Sobrenome = "Moreira de Alencar",
                        Email = "programador.alencar@hotmail.com",
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Diego",
                        Sobrenome = "Silva Sauro",
                        Email = "diego@hotmail.com",
                        Atribuicao = Enumerador.AtribuicaoEnum.PublicadorBatizado,
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Marcos",
                        Sobrenome = "Vinícius",
                        Email = "marcos@hotmail.com",
                        Atribuicao = Enumerador.AtribuicaoEnum.PublicadorBatizado,
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "José",
                        Sobrenome = "Andrade Filho",
                        Email = "jose@hotmail.com",
                        Atribuicao = Enumerador.AtribuicaoEnum.PioneiroAuxiliar30Horas,
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Matheus",
                        Sobrenome = "Souza Pinto",
                        Email = "matheus@hotmail.com",
                        Atribuicao = Enumerador.AtribuicaoEnum.PioneiroAuxiliar30Horas,
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Antonio",
                        Sobrenome = "Alvares Cabral",
                        Email = "cabral@hotmail.com",
                        Atribuicao = Enumerador.AtribuicaoEnum.PioneiroAuxiliar50Horas,
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Publicador.Inserir(new Model.Publicador()
                    {
                        Nome = "Robson",
                        Sobrenome = "Alencar Santos",
                        Email = "rosbon@hotmail.com",
                        Atribuicao = Enumerador.AtribuicaoEnum.PioneiroAuxiliar50Horas,
                        Telefone = 62985485652,
                        Nascimento = DateTime.Now,
                    });

                    Model.Relatorio.Inserir(new Model.Relatorio()
                    {
                         Id = 1,
                         Horas = 30
                    });
                    Model.Relatorio.Inserir(new Model.Relatorio()
                    {
                        Id = 2,
                        Horas = 8
                    });
                    Model.Relatorio.Inserir(new Model.Relatorio()
                    {
                        Id = 3,
                        Horas = 50
                    });
                    Model.Relatorio.Inserir(new Model.Relatorio()
                    {
                        Id = 4,
                        Horas = 12
                    });

                    Model.Relatorio.Inserir(new Model.Relatorio()
                    {
                        Id = 5,
                        Horas = 3
                    });

                    Model.Relatorio.Inserir(new Model.Relatorio()
                    {
                        Id = 6,
                        Horas = 20
                    });
                }
            }

            MainPage = new View.MasterDetailView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
