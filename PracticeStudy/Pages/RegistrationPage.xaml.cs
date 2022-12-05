using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PracticeStudy.Components;

namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
        
        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            string firstname = FirstNameTb.Text.Trim();
            string lastname = LastNameTb.Text.Trim();
            string patronymic = PatronymicTb.Text.Trim();
            string email = EmailTb.Text.Trim();
            //if (!CheckLogin(LoginTb.Text.ToString().Trim()))
            //{

            //}
            if (DBConnect.db.User.Local.Any(x => x.Login == login))
            {
                MessageBox.Show("Такой пользователь уже существует!");
            }
            else
            {
                if (login.Length > 0 && password.Length > 0 && firstname.Length > 0 && lastname.Length > 0 && patronymic.Length > 0 && email.Length > 0)
                            {
                                DBConnect.db.User.Add(new User
                                {
                                    Login = login,
                                    Password = password,
                                    FirstName = firstname,
                                    LastName = lastname,
                                    Patronymic = patronymic,
                                    Email = email,
                                    RoleId = 2
                                });


                                if (password.Length >= 6)
                                {
                                    
                                    bool symbol = false;
                                    bool number = false;
                                    bool IsAllUpper = false;
                                    for (int i = 0; i < password.Length; i++)
                                    {
                                        
                                        if (password[i] >= '0' && password[i] <= '9') number = true;
                                        if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%' || password[i] == '^') symbol = true;
                                        if (Char.IsUpper(password[i])) IsAllUpper = true;
                                    }
                                    
                                    if (!symbol)
                                        MessageBox.Show("Добавьте один из следующих символов: ! @ # $ % ^");
                                    else if (!number)
                                        MessageBox.Show("Добавьте хотя бы одну цифру");
                                    else if (!IsAllUpper)
                                        MessageBox.Show("Добавьте одну прописную букву");
                                    if (symbol && number && IsAllUpper)
                                    {
                                        MessageBox.Show("Пользователь успешно зарегестрирован!");
                                        DBConnect.db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пароль слишком короткий, требуется минимум 6 символов!");
                                }

                
                                Navigation.BackPage();
                            }
                            else
                            {
                                MessageBox.Show("Пусто!Пожалуйста, заполните поля");
                            }
            }
        
                
            
        }
        //public static List<Product> ItemsSource { get; private set; }
        //protected bool CheckLogin(string login)
        //{
        //    IEnumerable<Product> filterLogin = DBConnect.db.User.Where(x => x.Login.ToLower = login.ToLower);
            
        //    SqlConnection con = new SqlConnection(login);
        //    SqlCommand cmd = new SqlCommand("select id from users where lower(login) = lower(@login)", con);
        //    cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
        //    string id = "";
        //    try
        //    {
        //        con.Open();
        //        id = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        //...
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    if (String.IsNullOrEmpty(id)) return false;
        //    return true;
        //}

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginTb.Text = "";
            PasswordTb.Password = "";
            FirstNameTb.Text = "";
            LastNameTb.Text = "";
            PatronymicTb.Text = "";
            EmailTb.Text = "";
        }
        
    }
}
