using System;
using System.Drawing;
using System.Windows.Forms;

using FirstProgram.Src.Interfaces;

namespace FirstProgram.Src.View
{
    public class TelaInicio : IForm
    {
        public TelaInicio() : base ("Programa em CS", "Minha Janela", new Size(400, 350))
        {
            Console.WriteLine("SETUP_Rendering Interface");
            this.initializeInterface();
            Console.WriteLine("SETUP_Application - Ready\n\n");
        }

        protected void initializeInterface(){
            FlowLayoutPanel flexPanel = this.iFlexPanel("flexPanel");
            flexPanel.FlowDirection = FlowDirection.TopDown;
            flexPanel.BackColor = Color.White;
            flexPanel.ForeColor = Color.OrangeRed;

            
            // Initialize a subpanel in order to agroup subcomponents
            FlowLayoutPanel flexBox = this.iFlexPanel("menuPanel");
            flexBox.FlowDirection = FlowDirection.TopDown;
            flexBox.Controls.Add( this.iLabel("lblTitle", "Registrar Livro", this.fontTitleStyle) );
            flexBox.BackColor = Color.OrangeRed;
            flexBox.ForeColor = Color.White;


            FlowLayoutPanel boxInput = this.iFlexPanel("boxInput");

            // Book name input container
            FlowLayoutPanel boxNameInput = this.iFlexPanel("boxNameInput");
            boxNameInput.FlowDirection = FlowDirection.TopDown;
            boxNameInput.Controls.Add(this.iLabel("lblName", "Nome:"));
            boxNameInput.Controls.Add(this.txtName);

            // Book price input container
            FlowLayoutPanel boxPriceInput = this.iFlexPanel("boxPriceInput");
            boxPriceInput.FlowDirection = FlowDirection.TopDown;
            boxPriceInput.Controls.Add(this.iLabel("lblPrice", "Preço:"));
            boxPriceInput.Controls.Add(this.txtPrice);
    

            //Style
            this.btnSend.FlatStyle = FlatStyle.Flat;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.BackColor = Color.OrangeRed;
            this.btnSend.ForeColor = Color.White;

            this.btnSend.Anchor = AnchorStyles.Bottom;
            this.btnSend.Click += new EventHandler(onSendClick);
            
            
            // Add components into subpanel
            boxInput.Controls.Add(boxNameInput);
            boxInput.Controls.Add(boxPriceInput);
            boxInput.Controls.Add(this.btnSend);

            
            

            //Update interface
            flexPanel.Controls.Add(flexBox);
            flexPanel.Controls.Add(boxInput);
            Console.WriteLine("SETUP_Components - Ready");

            
            this.Controls.Add(flexPanel);
            Console.WriteLine("SETUP_Interface - Ready");
        }


        private void onSendClick(object sender, EventArgs e){
            String name = this.txtName.Text;
            String price = this.txtPrice.Text;
            Console.WriteLine($"APP_MSG: {name}");
            Console.WriteLine($"APP_MSG: {price}");
        }



        // Components declaration
        private TextBox txtName = new IForm().iText("txtName");
        private NumericUpDown txtPrice = new IForm().iNumber("txtPrice", 2);
        private Button btnSend = new IForm().iButton("btnSend", "Cadastrar"); 
    }
}