export const loadIntoLS = (den, val) => {
  localStorage.setItem(den, JSON.stringify(val))
}

export const readFromLS = (den) => {
  return JSON.parse(localStorage.getItem(den))
}

export const cleanLS = () => {
  localStorage.clear()
}

export const removeFromLS = (den) => {
  localStorage.removeItem(den)
}
export const SaveJwtToken = (tokenInput) => {
  if (GetJwtToken() === null) {
    const token = `Bearer ${tokenInput}`
    window.localStorage.setItem('jwtToken', token)
  } else {
    window.localStorage.removeItem('jwtToken')
    const token = `Bearer ${tokenInput}`
    window.localStorage.setItem('jwtToken', token)
  }
}

export const GetJwtToken = () => {
  return window.localStorage.getItem('jwtToken')
}
