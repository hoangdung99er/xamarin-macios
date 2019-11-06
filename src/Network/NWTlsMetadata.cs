//
// NWTlsMetadata.cs: Bindings the Netowrk nw_protocol_metadata_t API that is an Tls.
//
// Authors:
//   Manuel de la Pena <mandel@microsoft.com>
//
// Copyrigh 2019 Microsoft
//
using System;
using ObjCRuntime;
using Foundation;
using Security;
using CoreFoundation;

namespace Network {

	[TV (12,0), Mac (10,14), iOS (12,0), Watch (6,0)]
	public class NWTlsMetadata : NWProtocolMetadata {

		internal NWTlsMetadata (IntPtr handle, bool owns) : base (handle, owns) {}

		public SecProtocolMetadata SecProtocolMetadata
			=> new SecProtocolMetadata (nw_tls_copy_sec_protocol_metadata (GetCheckedHandle ()), owns: true);

	}
}