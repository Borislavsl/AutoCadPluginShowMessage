using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Drawing;

namespace AutoCADpluginShowMessage
{
    public partial class ShowMessageForm : Form
    {
        public ShowMessageForm() : base()
        {            
            InitializeComponent();
        }
        
        protected override void Dispose(bool disposing)
        {         
            base.Dispose(disposing);
        }       
        
        private Label _label;

        internal Label Label
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _label;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _label = value;             
            }
        }

        private Button _okButton;

        internal Button OkButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _okButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _okButton = value;                
            }
        }

        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            _label = new Label();
            _okButton = new Button();
            this.SuspendLayout();

            _label.Location = new Point(16, 16);
            _label.Name = "Label1";
            _label.Size = new Size(296, 23);
            _label.TabIndex = 0;
            _label.Text = "Show Message sample application";

            _okButton.CausesValidation = false;
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(168, 56);
            _okButton.Name = "OkButton";
            _okButton.TabIndex = 1;
            _okButton.Text = "Ok";
             
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(408, 86);
            this.Controls.AddRange(new Control[] { _okButton, _label });
            this.Name = "Form1";
            this.Text = "The message shown in AutoCad";
            this.ResumeLayout(false);
        }
    }
}
