import { urlBaseAccount } from "../ApiBase.js";
import { GetJwtToken } from "../LocalStorageService.js";

const token = GetJwtToken()

export const getAccountByCbu = async (cbu) => {

    const options = {
        method: 'GET',
        headers: {
            'Authorization': token,
        }
    }

    let r = await fetch(`${urlBaseAccount}/account/searchByCbu?cbu=${cbu}`, options)

    let response = {
        Status: r,
        Data: await r.json()
    }

    return response
}

export const getAccountByAlias = async (alias) => {

    const options = {
        method: 'GET',
        headers: {
            'Authorization': token,
        }
    }

    let r = await fetch(`${urlBaseAccount}/account/searchByAlias?alias=${alias}`, options)

    let response = {
        Status: r,
        Data: await r.json()
    }

    return response
}

export const getAccountByAccountId = async (accountId) => {

    const options = {
        method: 'GET',
        headers: {
            'Authorization': token,
        }
    }

    let response = await fetch(`${urlBaseAccount}/account/${accountId}`, options)

    return await response.json();
}

export const getAccountByCustomerId = async () => {

    const options = {
        method: 'GET',
        headers: {
            'Authorization': token,
        }
    }

    let response = await fetch(`${urlBaseAccount}/account/customerId`, options)
    return await response.json();
}