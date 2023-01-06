import { loadIntoSS, readFromSS } from "../SessionStorageService.js"
import { getAccountByAccountId } from "./AccountForTransactionServices.js"
import { getAccountByCustomerId } from "./AccountForTransactionServices.js"

export const loadDataStep1 = async (customerId, accountId) => {

    if (accountId != null) {
        let clientAccounts = await getAccountByCustomerId(customerId)
        let selectFromAccount = document.getElementById("cuenta-origen")

        let fullName

        clientAccounts.Data.map(async x => {
            let opt = document.createElement("option");
            let allInfoAccount = await getAccountByAccountId(accountId)

            opt.value = x.AccountId
            opt.text = x.AccountId + ` (${allInfoAccount.Data.Currency})`

            selectFromAccount.appendChild(opt)

            let inputBalance = document.getElementById("disponible-cuenta")

            if (accountId != null && accountId == x.AccountId) {
                opt.selected = true
                inputBalance.value = `${allInfoAccount.Data.Currency}$ ` + allInfoAccount.Data.Balance
            }

            fullName = allInfoAccount.Data.FullNameCustomer
            loadIntoSS("fullNameCustomer", fullName)
            document.getElementById("customerName").innerHTML = fullName
        })
    }
}

export const loadDataStep2 = async () => {
    let fromAccountSpan = document.getElementById("desde-cuenta")
    let toAccount = readFromSS("step-1-trx").toAccountId
    let amountSpan = document.getElementById("desde-monto")
    let nameReceiverDiv = document.getElementById("n-completo-receptor")
    let dniReceiverDiv = document.getElementById("dni-receptor")
    let accountReceiverDiv = document.getElementById("alias-o-cbu-receptor")

    let allInfoAccount = await getAccountByAccountId(toAccount)

    fromAccountSpan.innerHTML = readFromSS("step-1-trx").fromAccountId
    amountSpan.innerHTML = `${allInfoAccount.Data.Currency}$ ` + readFromSS("step-1-trx").amount
    nameReceiverDiv.innerHTML = allInfoAccount.Data.FullNameCustomer
    dniReceiverDiv.innerHTML = allInfoAccount.Data.Dni
    accountReceiverDiv.innerHTML = toAccount

    document.getElementById("customerName").innerHTML = readFromSS("fullNameCustomer")
}

export const loadDataStep3 = async () => {
    let fromAccount = readFromSS("step-1-trx").fromAccountId
    let toAccount = readFromSS("step-1-trx").toAccountId
    let fromAccountP = document.getElementById("fromAccount")
    let nameEmisor = document.getElementById("nameEmisor")
    let nameReceiver = document.getElementById("nameReceiver")
    let toAliasOrCbu = document.getElementById("toAliasOrCbu")
    let amount = document.getElementById("amount")
    let time = document.getElementById("time")
    let date = document.getElementById("date")
    let allInfoAccountEmisor = await getAccountByAccountId(fromAccount)
    let allInfoAccountReceiver = await getAccountByAccountId(toAccount)

    fromAccountP.innerHTML = fromAccount
    nameEmisor.innerHTML = allInfoAccountEmisor.Data.FullNameCustomer
    nameReceiver.innerHTML = allInfoAccountReceiver.Data.FullNameCustomer
    toAliasOrCbu.innerHTML = readFromSS("toAliasOrCbu")
    amount.innerHTML = `${allInfoAccountEmisor.Data.Currency}$ ` + readFromSS("step-1-trx").amount
    time.innerHTML = new Date().toLocaleTimeString()
    date.innerHTML = new Date().toLocaleDateString()

    document.getElementById("customerName").innerHTML = readFromSS("fullNameCustomer")
}