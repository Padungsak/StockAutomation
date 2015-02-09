using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Information
{
	internal class TFEXIndex
	{
		private long futureVol = 0L;
		private long futureOI = 0L;
		private long optionsVol = 0L;
		private long optionsOI = 0L;
		private long tfexTotalVol = 0L;
		private long tfexTotalOI = 0L;
		private long tfexTotalDeal = 0L;
		private string txiState;
		private int txiSession;
		private string txmState = string.Empty;
		private int txmSession;
		private string txrState = string.Empty;
		private int txrSession;
		private string txsState = string.Empty;
		private int txsSession;
		private string txeState = string.Empty;
		private int txeSession;
		private string txcState = string.Empty;
		private int txcSession;
		private Dictionary<string, BBOTFEXCurrency> bboCurrency = new Dictionary<string, BBOTFEXCurrency>();
		public long FutureVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.futureVol;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.futureVol = value;
			}
		}
		public long FutureOI
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.futureOI;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.futureOI = value;
			}
		}
		public long OptionsVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.optionsVol;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.optionsVol = value;
			}
		}
		public long OptionsOI
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.optionsOI;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.optionsOI = value;
			}
		}
		public long TfexTotalVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tfexTotalVol;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.tfexTotalVol = value;
			}
		}
		public long TfexTotalOI
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tfexTotalOI;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.tfexTotalOI = value;
			}
		}
		public long TfexTotalDeal
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tfexTotalDeal;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.tfexTotalDeal = value;
			}
		}
		public string TXIState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txiState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txiState = value;
			}
		}
		public int TXISession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txiSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txiSession = value;
			}
		}
		public string TXMState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txmState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txmState = value;
			}
		}
		public int TXMSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txmSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txmSession = value;
			}
		}
		public string TXRState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txrState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txrState = value;
			}
		}
		public int TXRSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txrSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txrSession = value;
			}
		}
		public string TXSState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txsState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txsState = value;
			}
		}
		public int TXSSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txsSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txsSession = value;
			}
		}
		public string TXEState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txeState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txeState = value;
			}
		}
		public int TXESession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txeSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txeSession = value;
			}
		}
		public string TXCState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txcState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txcState = value;
			}
		}
		public int TXCSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.txcSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.txcSession = value;
			}
		}
		public Dictionary<string, BBOTFEXCurrency> BBOCurrency
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.bboCurrency;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.bboCurrency = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TFEXIndex()
		{
		}
	}
}
