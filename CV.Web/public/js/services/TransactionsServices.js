import { urlBaseTrx } from "./ApiBase.js";
import { GetJwtToken } from "./LocalStorageService.js";

let token = GetJwtToken()

export const newTransaction = async (transaction) => {

    const options = {
        method: 'POST',
        headers: {
            'Authorization': token,
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(transaction)
    }

    let r = await fetch(`${urlBaseTrx}/Transaction`, options)

    let response = {
        Status: r,
        Data: await r.json()
    }

    return response

}

export const getAllHistory = async (accountId) => {

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

    let r = await fetch(`${urlBaseTrx}/MovementHistory/all`, options)

    let response = {
        Status: r,
        Data: await r.json()
    }

    return response
}

export const getHistoryFromDate = async (accountId, year, month, day) => {

    const account = {
        accountId: accountId,
        year: year,
        month: month,
        day: day
    }

    const options = {
        method: 'POST',
        headers: {
            'Authorization': token,
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(account)
    }

    let response = await fetch(`${urlBaseTrx}/MovementHistory/date`, options)

    return response.status
}