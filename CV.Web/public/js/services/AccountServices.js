import { urlBaseAccount, urlBaseTrx } from "./ApiBase.js";
import { GetJwtToken } from "./LocalStorageService.js"

let token = GetJwtToken()

export const GetAccountById = async (id) => {
    try {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Authorization':  token,
                'Content-Type': 'application/json;charset=utf-8' },
        }
        const response = await fetch(`${urlBaseAccount}/account/${id}`, requestOptions)  
        if (response.ok) {
            const data = await response.json()
            return data.Data
        }    
    }
    catch (error) {
        console.log(error)
    }
}

export const GetAccountsForCustomer = async () => {
    try {
        const options = {
            method: 'GET',
            headers: {
                'Authorization': token,
                'Content-Type': 'application/json;charset=utf-8'
            },
        }

        const response = await fetch(`${urlBaseAccount}/account/customerId`,options)
        if (response.ok) {
            const data = await response.json()
            return data.Data
        }    
    }
    catch (error) {
        console.log(error)
    }
}

export const DeleteAccount = async (id) => {
    try {   
        const options = {
            method: 'DELETE',
            headers: {
                'Authorization': token,
                'Content-Type': 'application/json;charset=utf-8'
            },
        }

        
        const response = await fetch(`${urlBaseAccount}/account/DeleteAccount?accountId=${id}`, options)

        if (response.ok) {
            const data = await response.json()
            return data.Data
        }    
    }
    catch (error) {
        console.log(error)
    }
}


export const UpdateAlias = async (accountId, aliasInput) => {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Authorization': token,
            'Content-Type': 'application/json;charset=utf-8' },
        body: JSON.stringify(
            {
                'accountId': accountId,
                'aliasInput':aliasInput
            }
        )
    }
    const response = await fetch(`${urlBaseAccount}/account/UpdateAlias?accountId=${accountId}&alias=${aliasInput}`, requestOptions)
    if (response.ok) {
        const data = await response.json()
        return data.Data
    }
}

export const UpdateBalance = async (accountId, amount) => {

    const requestOptions = {
        method: 'PUT',
        headers: {
            'Authorization': token,
            'Content-Type': 'application/json;charset=utf-8' },
        body: JSON.stringify(
            {
                'accountId': accountId,
                'amount': amount
            }
        )
    }
    const response = await fetch(`${urlBaseAccount}/account/UpdateBalance`, requestOptions)
    if (response.ok) {
        const data = await response.json()
        return data.Data
    }
} 

export const GetTransactionHistory = async (accountId) => {
    const account = {
        accountId: accountId,
    }

    const options = {
        method: 'POST',
        headers: {
            'Authorization': token,
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(account)
    }

    let response = await fetch(`${urlBaseTrx}/MovementHistory/all`, options)
    if (response.ok) {
        const data = await response.json()
        return data.data
    }
}

export const CreateAccount = async (currency) => {
    const options = {
        method: 'POST',
        headers: {
            'Authorization': token,
            'Content-Type': 'application/json;charset=utf-8'
        }
    }

    let response = await fetch(`${urlBaseAccount}/account?shortNameCurrency=${currency}`, options)
    if (response.ok) {
        const data = await response.json()
        return data.Data
    }
}
export const CreateAccount2 = async (currency) => {
    let url = `${urlBaseAccount}/account?shortNameCurrency=${currency}`
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
        console.log(body)
      })
      .finally((body) => {
        return body
      })
  }
