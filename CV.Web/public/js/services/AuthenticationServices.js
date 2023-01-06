import { GetJwtToken } from '../../js/services/LocalStorageService.js'

const urlBase = 'https://localhost:7132/api/Authentication'

export const Login = async (userAuth, callback) => {
  let url = `${urlBase}/login`
  url = encodeURI(url)
  const headersList = {
    'Content-Type': 'application/json',
    Accept: 'application/json'
  }
  await fetch(url, {
    method: 'POST',
    headers: headersList,
    body: JSON.stringify(userAuth)
  })
    .then((httpResponse) => {
      if (httpResponse.ok) {
        return httpResponse.json()
      }
      if (httpResponse.status === 400) {
        return httpResponse.json()
      }
    })
    .then((body) => {
      callback(body.data)
    })
    .finally((body) => {
      return body
    })
}

export const SignUp = async (userToSignUp, callback) => {
  let url = `${urlBase}/register`
  url = encodeURI(url)
  const headersList = {
    'Content-Type': 'application/json',
    Accept: 'application/json'
  }
  await fetch(url, {
    method: 'POST',
    headers: headersList,
    body: JSON.stringify(userToSignUp)
  })
    .then((httpResponse) => {
      if (httpResponse.status === 201) {
        return httpResponse.json()
      }
      if (httpResponse.status === 409) {
        return httpResponse.json()
      }
    })
    .then((body) => {
      callback(body)
    })
}

export const TokenIsValid = async (tokenRequest, callback) => {
  let url = `${urlBase}/token/isValid`
  const token = GetJwtToken()
  url = encodeURI(url)
  const headersList = {
    'Content-Type': 'application/json',
    Accept: 'application/json',
    Authorization: token
  }
  await fetch(url, {
    method: 'POST',
    headers: headersList,
    body: JSON.stringify(tokenRequest)
  })
    .then((httpResponse) => {
      if (httpResponse.ok) {
        return httpResponse.json()
      }
      if (httpResponse.status === 400) {
        return httpResponse.json()
      }
    })
    .then((body) => {
      callback(body.data)
    })
    .finally((body) => {
      return body
    })
}

export const VerifyEmailCode = async (code) => {
  let url = `${urlBase}/verifyEmail?token=${code}`
  const token = GetJwtToken()
  url = encodeURI(url)
  const headersList = {
    'Content-Type': 'application/json',
    Accept: 'application/json',
    Authorization: token
  }
  await fetch(url, {
    method: 'POST',
    headers: headersList
  })
    .then((httpResponse) => {
      if (httpResponse.ok) {
        return true
      }
      if (httpResponse.status === 400) {
        return false
      }
    })
}

export const ChangeState = async (state, callback) => {
  let url = `${urlBase}/changeState`
  const token = GetJwtToken()
  url = encodeURI(url)
  const headersList = {
    'Content-Type': 'application/json',
    Accept: 'application/json',
    Authorization: token
  }
  await fetch(url, {
    method: 'POST',
    headers: headersList,
    body: JSON.stringify(state)
  })
    .then((httpResponse) => {
      if (httpResponse.ok) {
        return httpResponse.json()
      }
      if (httpResponse.status === 400) {
        return httpResponse.json()
      }
    })
    .then((body) => {
      callback(body.data)
    })
    .finally((body) => {
      return body
    })
}