/**
 * When game is mounted in an `iframe` the code below
 * allows to receive session data from the parent window.
 */
window.addEventListener('message', event => {
  const { signal, data } = event.data
  switch (signal) {
    case "auth":
      return saveAuthInfo(data)
  }
})

const saveAuthInfo = data => {
  localStorage.setItem('web3Info', JSON.stringify(data))
}

window.getAuthInfo = () => {
  const data = localStorage.getItem('web3info')
  if (!data) {
    throw new Error('no auth data saved')
  }
  return data
}

window.clearAuthInfo = () => {
  localStorage.delete('web3info')
}