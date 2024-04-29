var FileIO = {
 OpenMetamaskLogin: function() {
        // Check if the metamask plugin is available
        if (!window.ethereum) {
            console.log("No metamask plugin found");
            return; // Return nothing if plugin not found
        }

        // Request accounts asynchronously
        window.ethereum
            .request({ method: 'eth_requestAccounts' })
            .then(function(accounts) {
                // If accounts are not obtained, log an error and call the callback with error message
                if (!accounts) {
                    console.log("Login failed");
                    return; // Return nothing if login failed
                }
            })
            .catch(function(error) {
                // Handle errors caught during the asynchronous operation
                 console.error(error.message);
            });
    },

  SaveToLocalStorage : function(key, data) {
    localStorage.setItem(UTF8ToString(key), UTF8ToString(data));
  },

  LoadFromLocalStorage : function(key) {
    var returnStr = localStorage.getItem(UTF8ToString(key));
    if (!returnStr) {
        return null;
    }
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  RemoveFromLocalStorage : function(key) {
    localStorage.removeItem(UTF8ToString(key));
  },

  HasKeyInLocalStorage : function(key) {
    if (localStorage.getItem(UTF8ToString(key))) {
      return 1;
    }
    else {
      return 0;
    }
  }
};

mergeInto(LibraryManager.library, FileIO);;