using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BankServices.Loans;
using BankServices.Loans.MortgageLoan;
using BankServices.Loans.AnnualSeries;

namespace BankServices.Loans.WindowsApplication
{
	/// <summary>
	/// Summary description for LoanTool.
	/// </summary>
	public class LoanTool : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelAmount;
		private System.Windows.Forms.Label labelPeriod;
		private System.Windows.Forms.TextBox textBoxAmount;
		private System.Windows.Forms.TextBox textBoxPeriod;
		private System.Windows.Forms.Button buttonCalculate;
		private System.Windows.Forms.ListView listViewPayments;
		private System.Windows.Forms.ColumnHeader columnPaymentNumber;
		private System.Windows.Forms.ColumnHeader columnLeft;
		private System.Windows.Forms.ColumnHeader columnAmount;
		private System.Windows.Forms.ColumnHeader columnInterest;
		private System.Windows.Forms.ColumnHeader columnTotal;
		private System.Windows.Forms.ComboBox comboBoxLoanTypes;


		private BankServices.Loans.MortgageLoan.Mortgage mortgage;
		private BankServices.Loans.AnnualSeries.AnnualSeriesRepayment mortgagePayment;
		private System.Windows.Forms.ErrorProvider myErrorProvider;



		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public LoanTool()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			AdditionalInit();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxAmount = new System.Windows.Forms.TextBox();
			this.labelAmount = new System.Windows.Forms.Label();
			this.labelPeriod = new System.Windows.Forms.Label();
			this.textBoxPeriod = new System.Windows.Forms.TextBox();
			this.buttonCalculate = new System.Windows.Forms.Button();
			this.listViewPayments = new System.Windows.Forms.ListView();
			this.columnPaymentNumber = new System.Windows.Forms.ColumnHeader();
			this.columnLeft = new System.Windows.Forms.ColumnHeader();
			this.columnAmount = new System.Windows.Forms.ColumnHeader();
			this.columnInterest = new System.Windows.Forms.ColumnHeader();
			this.columnTotal = new System.Windows.Forms.ColumnHeader();
			this.comboBoxLoanTypes = new System.Windows.Forms.ComboBox();
			this.myErrorProvider = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// textBoxAmount
			// 
			this.textBoxAmount.Location = new System.Drawing.Point(128, 104);
			this.textBoxAmount.Name = "textBoxAmount";
			this.textBoxAmount.Size = new System.Drawing.Size(104, 20);
			this.textBoxAmount.TabIndex = 1;
			this.textBoxAmount.Text = "";
			this.textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxAmount.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAmount_Validating);
			this.textBoxAmount.Validated += new System.EventHandler(this.textBoxAmount_Validated);
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(32, 104);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(96, 24);
			this.labelAmount.TabIndex = 4;
			this.labelAmount.Text = "Amount";
			// 
			// labelPeriod
			// 
			this.labelPeriod.Location = new System.Drawing.Point(32, 144);
			this.labelPeriod.Name = "labelPeriod";
			this.labelPeriod.Size = new System.Drawing.Size(96, 24);
			this.labelPeriod.TabIndex = 5;
			this.labelPeriod.Text = "Period (months)";
			// 
			// textBoxPeriod
			// 
			this.textBoxPeriod.Location = new System.Drawing.Point(128, 144);
			this.textBoxPeriod.Name = "textBoxPeriod";
			this.textBoxPeriod.Size = new System.Drawing.Size(104, 20);
			this.textBoxPeriod.TabIndex = 2;
			this.textBoxPeriod.Text = "";
			this.textBoxPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBoxPeriod.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPeriod_Validating);
			this.textBoxPeriod.Validated += new System.EventHandler(this.textBoxPeriod_Validated);
			// 
			// buttonCalculate
			// 
			this.buttonCalculate.Location = new System.Drawing.Point(72, 192);
			this.buttonCalculate.Name = "buttonCalculate";
			this.buttonCalculate.Size = new System.Drawing.Size(144, 24);
			this.buttonCalculate.TabIndex = 3;
			this.buttonCalculate.Text = "Calculate";
			this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
			// 
			// listViewPayments
			// 
			this.listViewPayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnPaymentNumber,
																							   this.columnLeft,
																							   this.columnAmount,
																							   this.columnInterest,
																							   this.columnTotal});
			this.listViewPayments.FullRowSelect = true;
			this.listViewPayments.GridLines = true;
			this.listViewPayments.Location = new System.Drawing.Point(288, 24);
			this.listViewPayments.Name = "listViewPayments";
			this.listViewPayments.Size = new System.Drawing.Size(464, 432);
			this.listViewPayments.TabIndex = 6;
			this.listViewPayments.View = System.Windows.Forms.View.Details;
			// 
			// columnPaymentNumber
			// 
			this.columnPaymentNumber.Text = "Payment";
			this.columnPaymentNumber.Width = 59;
			// 
			// columnLeft
			// 
			this.columnLeft.Text = "Left";
			this.columnLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnLeft.Width = 100;
			// 
			// columnAmount
			// 
			this.columnAmount.Text = "Refund";
			this.columnAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnAmount.Width = 100;
			// 
			// columnInterest
			// 
			this.columnInterest.Text = "Interest";
			this.columnInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnInterest.Width = 100;
			// 
			// columnTotal
			// 
			this.columnTotal.Text = "Total";
			this.columnTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnTotal.Width = 100;
			// 
			// comboBoxLoanTypes
			// 
			this.comboBoxLoanTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLoanTypes.Location = new System.Drawing.Point(32, 24);
			this.comboBoxLoanTypes.Name = "comboBoxLoanTypes";
			this.comboBoxLoanTypes.Size = new System.Drawing.Size(216, 21);
			this.comboBoxLoanTypes.TabIndex = 0;
			// 
			// myErrorProvider
			// 
			this.myErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.myErrorProvider.ContainerControl = this;
			// 
			// LoanTool
			// 
			this.AcceptButton = this.buttonCalculate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(768, 461);
			this.Controls.Add(this.comboBoxLoanTypes);
			this.Controls.Add(this.listViewPayments);
			this.Controls.Add(this.buttonCalculate);
			this.Controls.Add(this.textBoxPeriod);
			this.Controls.Add(this.labelPeriod);
			this.Controls.Add(this.labelAmount);
			this.Controls.Add(this.textBoxAmount);
			this.Name = "LoanTool";
			this.Text = "Loan Tool";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new LoanTool());
		}

		private void AdditionalInit()
		{
			// this.mortgage
			mortgage = new Mortgage();

			//this.mortgagePayment
			mortgagePayment = new AnnualSeriesRepayment(mortgage);

			// this.comboBoxLoanTypes
			this.comboBoxLoanTypes.Items.Add(mortgage);
			this.comboBoxLoanTypes.SelectedIndex = 0;
		}


		private void buttonCalculate_Click(object sender, System.EventArgs e)
		{
			this.mortgagePayment.loan.Period = UInt32.Parse(this.textBoxPeriod.Text);
			this.mortgagePayment.loan.Amount = Decimal.Parse(this.textBoxAmount.Text);
			this.listViewPayments.Items.Clear();
			this.listViewPayments.Items.AddRange(this.getListViewItemPayments(mortgagePayment));
		}

		private void textBoxAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string errorMessage;
			if (!ValidAmount(this.textBoxAmount.Text, out errorMessage))
			{
				e.Cancel = true;
				this.textBoxAmount.Select(0, this.textBoxAmount.Text.Length);
				this.myErrorProvider.SetError(this.textBoxAmount, errorMessage);
			}
		}

		private void textBoxAmount_Validated(object sender, System.EventArgs e)
		{
			this.myErrorProvider.SetError(this.textBoxAmount,"");
		}

		private void textBoxPeriod_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string errorMessage;
			if (!ValidPeriod(this.textBoxPeriod.Text, out errorMessage))
			{
				e.Cancel = true;
				this.textBoxPeriod.Select(0, this.textBoxPeriod.Text.Length);
				this.myErrorProvider.SetError(this.textBoxPeriod, errorMessage);
			}		
		}

		private void textBoxPeriod_Validated(object sender, System.EventArgs e)
		{
			this.myErrorProvider.SetError(this.textBoxPeriod, "");
		}


		private System.Windows.Forms.ListViewItem [] getListViewItemPayments(BankServices.Loans.AnnualSeries.AnnualSeriesRepayment loanPayment)
		{
			System.Windows.Forms.ListViewItem [] payments = new System.Windows.Forms.ListViewItem[loanPayment.loan.Period+1];

			decimal mrate;
			decimal minterest;
			decimal mtotal;
			decimal mleft;
			decimal totalInterest=0;
			int i;


			for (i = 1; i <= loanPayment.loan.Period; i++)
			{
				mrate = loanPayment.getPeriodRefund((uint)i);
				minterest = loanPayment.getPeriodInterest((uint)i);
				mtotal = loanPayment.getPeriodPayment((uint)i);
				mleft = loanPayment.getLeftAmount((uint) i);
				totalInterest += minterest;
				payments[i-1] = new System.Windows.Forms.ListViewItem(i.ToString("D"));
				payments[i-1].SubItems.Add(mleft.ToString("C"));
				payments[i-1].SubItems.Add(mrate.ToString("C"));
				payments[i-1].SubItems.Add(minterest.ToString("C"));
				payments[i-1].SubItems.Add(mtotal.ToString("C"));
			}

			decimal total = loanPayment.loan.Amount+totalInterest;
			payments[i-1] = new System.Windows.Forms.ListViewItem("TOTAL");
			payments[i-1].SubItems.Add("-");
			payments[i-1].SubItems.Add(loanPayment.loan.Amount.ToString("C"));
			payments[i-1].SubItems.Add(totalInterest.ToString("C"));
			payments[i-1].SubItems.Add(total.ToString("C"));

			return payments;
		}

		private bool ValidAmount(string text, out string errMessage)
		{
			bool isValid = true;
			errMessage = "";
			
			try
			{
				decimal amount = Decimal.Parse(text);
			}
			catch (ArgumentNullException e)
			{
				errMessage = "Null Argument";
				isValid = false;
			}
			catch (FormatException e)
			{
				errMessage = "Wrong Format";
				isValid = false;
				}
			catch (OverflowException e)
			{
				errMessage = "Number too big";
				isValid = false;
			}
			return isValid;
		}

		private bool ValidPeriod(string text, out string errMessage)
		{
			bool isValid = true;
			errMessage = "";

			try
			{
				uint period = UInt32.Parse(text);
			}
			catch (ArgumentNullException e)
			{
				errMessage = "Null Argument";
				isValid = false;
			}
			catch (FormatException e)
			{
				errMessage = "Wrong Format";
				isValid = false;
			}
			catch (OverflowException e)
			{
				errMessage = "Number too big";
				isValid = false;
			}
			return isValid;
		}

	}
}
