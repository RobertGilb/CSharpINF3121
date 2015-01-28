using System;
using BankServices.Loans;

namespace BankServices.Loans.MortgageLoan
{
	/// <summary>
	/// Defines a mortgage loan
	/// <code>public class Mortgage : BankServices.Loans.LoanInfo</code>
	/// </summary>
	public class Mortgage : BankServices.Loans.LoanInfo
	{
		#region Protected Members
		/// <summary>
		/// The minimum amount to be loaned
		/// </summary>
		protected const decimal minAmount = 10000M;
		/// <summary>
		/// The maximum amount to be loaned
		/// </summary>
		protected const decimal maxAmount = 2000000M;

		/// <summary>
		/// The minimum repayment period
		/// </summary>
		protected const uint minPeriod = 12;
		/// <summary>
		/// The maximum repayment period
		/// </summary>
		protected const uint maxPeriod = 240;
		/// <summary>
		/// The default interest Rate
		/// </summary>
		protected const decimal defaultInterestRate = 3.5M;
		#endregion

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public Mortgage() : base(Mortgage.defaultInterestRate, Mortgage.minAmount, Mortgage.maxPeriod)
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_interestRate"></param>
		public Mortgage(decimal _interestRate) : base (_interestRate, Mortgage.minAmount, Mortgage.minPeriod)
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_interestRate"></param>
		/// <param name="_amount"></param>
		/// <param name="_period"></param>
		public Mortgage(decimal _interestRate, decimal _amount, uint _period) : base(_interestRate)
		{
			this.Amount = _amount;
			this.Period = _period;
		}

		#endregion

		#region Accessors
		/// <summary>
		/// 
		/// </summary>
		public override decimal Amount
		{
			get
			{
				return base.Amount;
			}
			set
			{
				if (value <= maxAmount && value >= minAmount)
				{
					base.Amount = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public override uint Period
		{
			get
			{
				return base.Period;
			}
			set
			{
				if (value <= maxPeriod && value >= minPeriod)
				{
					base.Period = value;
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Mortgage (Interest Rate = " + this.InterestRate.ToString() + ")";
		}
		#endregion
	}
}
namespace BankServices.Loans.AnnualSeries
{

	/// <summary>
	/// Defines an annual series repayment plan
	/// </summary>
	public class AnnualSeriesRepayment : BankServices.Loans.LoanPaymentStrategy
	{
		#region Protected Members
		/// <summary>
		/// The interest rate for a period unit.
		/// </summary>
		protected decimal periodInterestRate;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of AnnualSeriesRepayment given the specified LoanInfo object
		/// </summary>
		/// <param name="loan"></param>
		public AnnualSeriesRepayment(LoanInfo loan) : base(loan)
		{
			periodInterestRate = loan.InterestRate/(decimal)12;
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Returns the amount payed as load refund in the specified period unit (month).
		/// </summary>
		/// <param name="periodUnit"></param>
		/// <returns></returns>
		public override decimal getPeriodRefund(uint periodUnit)
		{
			return (decimal) loan.Amount/loan.Period;
		}
		/// <summary>
		/// Returns the interest to be payed in the specified period unit(month).
		/// </summary>
		/// <param name="periodUnit"></param>
		/// <returns></returns>
		public override decimal getPeriodInterest(uint periodUnit)
		{
			decimal aux = (decimal)(loan.Period-periodUnit+1)/loan.Period;
			return loan.Amount * aux * ((decimal)periodInterestRate/100);
		}
		/// <summary>
		/// Returns the amount to be payed in the specified period unit(month).
		/// </summary>
		/// <param name="periodUnit"></param>
		/// <returns></returns>
		public override decimal getPeriodPayment(uint periodUnit)
		{
			return this.getPeriodRefund(periodUnit) + this.getPeriodInterest(periodUnit);
		}
		/// <summary>
		/// Returns the amount left to be payed in the specified period unit (month).
		/// </summary>
		/// <param name="periodUnit"></param>
		/// <returns></returns>
		public decimal getLeftAmount(uint periodUnit)
		{
			return loan.Amount - (decimal)periodUnit * this.getPeriodRefund(periodUnit);
		}
		#endregion

	}


	/// <summary>
	/// Defines an annual series repayment plan.
	/// Same as AnnualSeriesRepament class with an extra public method which will ease displaying
	/// the repayment plan in a ListView control
	/// </summary>
	class VisualAnnualSeriesRepayment : AnnualSeriesRepayment
	{
		#region Constructors
		/// <summary>
		/// Creates a new instance of VisualAnnualSeriesRepayment given the specified LoanInfo object
		/// </summary>
		/// <param name="loan"></param>
		public VisualAnnualSeriesRepayment(LoanInfo loan) : base(loan)
		{
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returns an array of System.Windows.Fors.ListViewItem objects.
		/// Each ListViewItem object has 4 subitems defined: the left amount to be payed, monthly
		/// loan refund, monthly interest and the total amount to be payed
		/// </summary>
		/// <returns></returns>
		public System.Windows.Forms.ListViewItem[] getListViewItemPayment()
		{
			System.Windows.Forms.ListViewItem [] payments = new System.Windows.Forms.ListViewItem[loan.Period];

			decimal mrate;
			decimal minterest;
			decimal mtotal;
			decimal mleft;
			decimal totalInterest=0;


			for (int i = 1; i <= loan.Period; i++)
			{
				mrate = base.getPeriodRefund((uint)i);
				minterest = base.getPeriodInterest((uint)i);
				mtotal =base.getPeriodPayment((uint)i);
				mleft = base.getLeftAmount((uint) i);
				totalInterest += minterest;
				payments[i-1] = new System.Windows.Forms.ListViewItem(i.ToString("D"));
				payments[i-1].SubItems.Add(mleft.ToString("C"));
				payments[i-1].SubItems.Add(mrate.ToString("C"));
				payments[i-1].SubItems.Add(minterest.ToString("C"));
				payments[i-1].SubItems.Add(mtotal.ToString("C"));
			}

			int l = payments.Length;;
			decimal total = loan.Amount+totalInterest;
			payments[l-1] = new System.Windows.Forms.ListViewItem("TOTAL");
			payments[l-1].SubItems.Add("-");
			payments[l-1].SubItems.Add(loan.Amount.ToString("C"));
			payments[l-1].SubItems.Add(totalInterest.ToString("C"));
			payments[l-1].SubItems.Add(total.ToString("C"));

			return payments;
		}
		#endregion
	}
}