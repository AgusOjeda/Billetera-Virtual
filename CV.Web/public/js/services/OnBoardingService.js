import { GetJwtToken } from '../../js/services/LocalStorageService.js'
import { loadIntoSS } from './SessionStorageService.js'

const urlBase = 'https://localhost:7103/api/Customer'

export const CreateCustomer = async (customer, callback) => {
  let url = `${urlBase}`
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
    body: JSON.stringify(customer)
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
      loadIntoSS("customerId", body.data)
      callback(body)
    })
    .finally((body) => {
      return body
    })
}

export const SetVerification = async (callback) => {
  let url = `${urlBase}/VerificationSet`
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
        return httpResponse.json()
      }
      if (httpResponse.status === 400) {
        return httpResponse.json()
      }
    })
    .then((body) => {
      callback(body)
    })
    .finally((body) => {
      return body
    })
}

export const PostAddress = async (address, callback) => {
  let url = `${urlBase}/Address`
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
    body: JSON.stringify(address)
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
      callback(body)
    })
    .finally((body) => {
      return body
    })
}
