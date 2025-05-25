using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static PhoneBook.Form1;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        public Contacts head;
        public int size;


        // The node class
        public class Contacts
        {
            public string FirstName;
            public string LastName;
            public string Email;
            public string PhoneNumber;
            public Contacts Next;



            public Contacts(string firstName, string lastName, string email, string phoneNumber)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                PhoneNumber = phoneNumber;


            }
        }

        public Form1()
        {

            InitializeComponent();
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (FirstNametextBox.Text == "" || PhoneNumbertextBox.Text == "") 
            {
                MessageBox.Show("At least First Name and Phone number Required to save ");
                FirstNametextBox.Focus();
            }
            else
            {
                if (long.TryParse(PhoneNumbertextBox.Text, out long isparsable))
                {
                    insert(FirstNametextBox.Text, LastNametextBox.Text, EmailtextBox.Text, PhoneNumbertextBox.Text);
                    FirstNametextBox.Text = "";
                    LastNametextBox.Text = "";
                    EmailtextBox.Text = "";
                    PhoneNumbertextBox.Text = "";
                    MessageBox.Show("Saved succesfully");
                    PhoneErrorlabel.Hide();
                }
                else
                {
                    PhoneNumbertextBox.Text = "";
                    PhoneErrorlabel.Show();
                    PhoneNumbertextBox.Focus();

                }
            }

        
        }

        private void Newbutton_Click(object sender, EventArgs e)
        {
            FirstNametextBox.Text = "";
            LastNametextBox.Text = "";
            EmailtextBox.Text = "";
            PhoneNumbertextBox.Text = "";

        }

        private void Loadbutton_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            displayContacts();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FirstNametextBox.Focus();
            dataGridView1.Hide();
            UPDATEpanel1.Hide();
            PhoneErrorlabel.Hide();


        }

        public void insert(string FirstName, string LastName, string Email, string PhoneNumber)
        {
            Contacts contacts = new Contacts(FirstName, LastName, Email, PhoneNumber);
            Contacts temp = head;
            if (head == null)
            {
                head = contacts;

            }

            else
            {
                if (String.Compare(temp.FirstName, contacts.FirstName) > 0)
                {
                    contacts.Next = head;
                    head = contacts;
                }
                else
                {
                    while (temp.Next != null && String.Compare(temp.Next.FirstName, contacts.FirstName) < 0)
                    {
                        temp = temp.Next;
                    }

                    contacts.Next = temp.Next;
                    temp.Next = contacts;
                }
            }
          //  size++;
        }
        public void displayContacts()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Show();
            Contacts temp = head;
            int i = 0;

            while (temp!=null)
            {
                 dataGridView2.Rows.Add(temp.FirstName, temp.LastName, temp.Email, temp.PhoneNumber);
                temp = temp.Next;
            }

        }

        public bool SearchContact(string name)
        {
            Contacts srch = head;


            if (srch == null)
            {
                MessageBox.Show("Contact's Empty");

            }
            else
            {
                try
                {


                    while (srch != null)


                    {
                        if (srch.FirstName.Contains(name) || (srch.LastName.Contains(name)))
                        {

                            dataGridView1.Rows.Add(srch.FirstName, srch.LastName, srch.Email, srch.PhoneNumber);
                            dataGridView1.Show();

                        }
                        srch = srch.Next;
                    }
                    if (dataGridView1.Rows.Count != 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invald name");
                }

            }
                
                
            return false;

        }
        public void searchcontactphone(string phone)
        {
            Contacts srch = head;


            if (srch == null)
            {
                MessageBox.Show("Contact's Empty");

            }
            else
            {
                try
                {


                    while (srch != null)


                    {
                        if (srch.PhoneNumber.Contains(phone))
                        {

                            dataGridView1.Rows.Add(srch.FirstName, srch.LastName, srch.Email, srch.PhoneNumber);
                            dataGridView1.Show();

                        }
                        srch = srch.Next;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ivalid Phone Number");
                }
            }


        }


        private void Searchbutton_Click(object sender, EventArgs e)
        { dataGridView1.Rows.Clear();
            dataGridView2.Hide();
            if (string.IsNullOrEmpty(SearchcomboBox.Text))
            {
                MessageBox.Show("No filter selected");
            }
            else
            {
                if (SearchcomboBox.SelectedItem == "By Name")
                {
                    if (string.IsNullOrEmpty(SearchtextBox.Text))
                    {
                        MessageBox.Show("Enter a Name to Search");
                    }
                    else
                    {
                    SearchContact(SearchtextBox.Text);
                    }
                    
                }

                if (SearchcomboBox.SelectedItem == "By Phone Number")
                {
                    if (string.IsNullOrEmpty(SearchtextBox.Text))
                    {
                        MessageBox.Show("Enter a Phone Number to Search");
                    }
                    else
                    {
                       searchcontactphone(SearchtextBox.Text);
                    }
                  
                }
            }
        }
        public void DeletContacts(string name)
        {
            if (!SearchContact(name))
            {
                MessageBox.Show("Contact not on the list");
            }
            else

            {
                Contacts temp = head;
                try
                {

                
                    if (temp.FirstName == name)
                    {
                        head = head.Next;
                    }
                    else {

                        while (temp.Next != null && temp.Next.FirstName != name)
                        {
                            temp = temp.Next;
                        }
                        temp.Next = temp.Next.Next;

                        DelettextBox1.Text = "";
                      //  size--;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ivalid Name");
                }
            }

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {

            Contacts temp = head;
            if (temp == null)
            {
                MessageBox.Show("Contact is empty");
            }
            else 
            {
                    while (temp != null)
                    {
                        if (temp.FirstName == DelettextBox1.Text)
                        {
                            DeletContacts(DelettextBox1.Text);
                            displayContacts();
                            DelettextBox1.Text = "";
                            MessageBox.Show("Contact deleted");

                        }

                        temp = temp.Next;
                    }
           
                
            }

        }
    

        private void Updatelink_Click(object sender, EventArgs e)
        {
            displayContacts();
            UPDATEpanel1.Show();
            Updatepanel.Hide();
            SearchcomboBox.Hide();
            Updatebutton.Hide();
            label7.Hide();
           
        }

        private void Enterbutton_Click(object sender, EventArgs e)
        {

            if (!SearchContact(FirstNameEnter.Text))
            {
                MessageBox.Show("Contact not on the list");
            }
            else 
            { 
            
                    Contacts temp = head;
                   
                     while (temp != null)
                         {
                            if (temp.FirstName == FirstNameEnter.Text)
                            {
                                Updatepanel.Show();
                                Updatebutton.Show();
      
                                dataGridView2.Show();

                              }

                            temp = temp.Next;
                     }          
           
            }
                
        }
        public void Update()
        {
           String name=FirstNameEnter.Text;
            Contacts temp = head;
            while (temp != null)
            {
                if (temp.FirstName == name)
                {
                    temp.FirstName = FirstNametextBoxu.Text;
                    temp.LastName = LastNameU.Text;
                    temp.Email = EmailU.Text;
                    temp.PhoneNumber = PhoneNumberU.Text;
                }

                temp = temp.Next;

            }         

        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            if (FirstNametextBoxu.Text == "" || PhoneNumberU.Text == "")
            {
                MessageBox.Show("At least First Name and Phone number Required to save ");
                FirstNametextBox.Focus();
            }
            else
            {
                if (long.TryParse(PhoneNumberU.Text, out long isparsable))
                {

                    Update();
                    FirstNametextBoxu.Text = "";
                    LastNameU.Text = "";
                    EmailU.Text = "";
                    PhoneNumberU.Text = "";
                    FirstNameEnter.Text = "";
                    Updatepanel.Hide();
                    Updatebutton.Hide();
                    displayContacts();
                    label7.Hide();
                    MessageBox.Show("Contact Updated");

                }
                else
                {
                    PhoneNumberU.Text = "";
                    label7.Show();
                    PhoneNumberU.Focus();

                }
            }
           
        }

        private void Backbutton_Click(object sender, EventArgs e)
        {
            UPDATEpanel1.Hide();
            Updatepanel.Show();
            SearchcomboBox.Show();
            Updatebutton.Show();
        }
    }   
}

