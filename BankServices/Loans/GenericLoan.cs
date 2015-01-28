using System;

namespace BankServices.Loans
{
	/// <summary>
	/// <para>Generic definition for a loan.</para>
	/// <para>Can be instantiated for simple loan definitions. For more complex loans, LoanInfo will serve as base class.</para>
	/// <seealso cref="BankServices.Loans.MortgageLoan.Mortgage"/>
	/// </summary>
	public class LoanInfo
	{
		#region Protected Members
		/// <summary>
		/// Amount of money to be loaned
		/// </summary>
		protected decimal amount;
		/// <summary>
		/// Number of months for repayment
		/// </summary>
		protected uint period;
		/// <summary>
		/// Annual interest rate 
		/// </summary>
		protected decimal interestRate;
		#endregion

		#region Constructors
		/// <summary>
		/// <para>Initializes a new instance of LoanInfo.</para>
		/// <para>Members are set to 0.</para>
		/// </summary>
		public LoanInfo()
		{
			amount = 0;
			period = 0;
			interestRate = 0;
		}
		/// <summary>
		/// Initializes a new instance of LoanInfo given the specified annual interest rate
		/// </summary>
		/// <param name="_interestRate">The annual interest rate</param>
		public LoanInfo(decimal _interestRate) : base()
		{
			this.InterestRate = _interestRate;
		}
		/// <summary>
		/// Initializes a new instance of LoanInfo give the specified annual interest rate, amount and repayment period in months
		/// </summary>
		/// <param name="_interestRate"> The annual interest rate. </param>
		/// <param name="_amount">The amount to be loaned. </param>
		/// <param name="_period"> The period of loan repayment. </param>
		public LoanInfo(decimal _interestRate, decimal _amount, uint _period) : this(_interestRate)
		{
			this.Amount = _amount;
			this.Period = _period;
		}
		#endregion

		#region Accessors
		///<value> Gets or sets the annual interest rate (must be a number between 0 and 100).</value>
		public virtual decimal InterestRate
		{
			get
			{
				return interestRate;
			}
			set
			{
				if (value <= 100 && value >=0)
				{
					interestRate = value;
				}
			}
		}
		/// <value> Gets or sets the amount of money to be loaned.</value>
		public virtual decimal Amount
		{
			get
			{
				return amount;
			}
			set
			{
				amount = value;
			}
		}
		/// <value> Gets or sets the period of loan repayment.</value>
		public virtual uint Period
		{
			get
			{
				return period;
			}
			set
			{
				period = value;
			}
		}
		#endregion
	}

	/// <summary>
	/// Generic definition for a repayment plan.
	/// </summary>
	public abstract class LoanPaymentStrategy
	{
		#region Public Members
		/// <summary>
		/// Loan to be payed
		/// </summary>
		public LoanInfo loan;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of LoanPaymentStrategy given the specified LoanInfo object
		/// </summary>
		/// <param name="newLoan"></param>
		public LoanPaymentStrategy(LoanInfo newLoan)
		{
			loan = new LoanInfo();
			loan.Amount = newLoan.Amount;
			loan.InterestRate = newLoan.InterestRate;
			loan.Period = newLoan.Period;
		}
		#endregion

		#region Public Methods
		#region Abstract Methods
		/// <summary>
		/// Returns the amount to be payed in the specified period unit
		/// </summary>
		/// <param name="periodUnit"></param>
		/// <returns></returns>
		public abstract decimal getPeriodPayment(uint periodUnit);
		/// <summary>
		/// Returns the interest to be payed in the specified period unit.
		/// </summary>
		/// <param name="periodUnit"></param>
		/// <returns></returns>
		public abstract decimal getPeriodInterest(uint periodUnit);
		/// <summary>
		/// Returns the amount payed as load refund in the specified period unit.
		/// </summary>
		/// <param name="periodUnit"></param>
		/// <returns></returns>
		public abstract decimal getPeriodRefund(uint periodUnit);
		#endregion
		#endregion
	}
	/// <summary>
	/// Offers support for implementing additional cheking for a loan, if necessary
	/// </summary>
	public interface ILoanValidator
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		bool IsValid();
	}
}