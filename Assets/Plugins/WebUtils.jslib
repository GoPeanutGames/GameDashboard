mergeInto(LibraryManager.library, {
    IsMobile: function(){
        return /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
    },
	
	RequestAuthenticationInfo: async function (cbSuccess, cbFailure) {
		try 
		{
			// throws error if no auth info saved
			let info = window.getAuthInfo();
		
			const bufferSize = lengthBytesUTF8(info) + 1;
            const buffer = _malloc(bufferSize);
            stringToUTF8(info, buffer, bufferSize);
            Module.dynCall_vi(cbSuccess, [buffer]);
		} 
		catch (e) {
			console.log("Failed to get saved auth info:", e.message)
			Module.dynCall_vi(cbFailure);
		}
	}
});