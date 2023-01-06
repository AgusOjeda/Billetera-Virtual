import { GetAccountById } from "../AccountServices.js"
import { cleanSS, readFromSS } from "../SessionStorageService.js"
import { GetAccountsForCustomer } from "../AccountServices.js"
import { cardAccount } from "../../components/account/CardAccount.js"
import { loadIntoSS } from "../../services/SessionStorageService.js"
import { redirectTo } from "../../services/RedirectService.js"
import { cleanLS } from "../LocalStorageService.js"
import { getAllHistory } from "../TransactionsServices.js"

let customerId

if (readFromSS("customerId") != null)
    customerId = readFromSS("customerId")
else
    customerId = "855C3653-CC51-4D58-8D5B-59227C1826F5"

loadIntoSS("customerId", customerId)

export const loadCards = async () => {

    let clientAccounts = await GetAccountsForCustomer()

    let fullName

    clientAccounts.map(async x => {

        let allInfoAccount = await GetAccountById(x.AccountId)

        let card = cardAccount(allInfoAccount, x.AccountId)

        document.getElementById("cards-account").insertAdjacentHTML("beforeend", card)

        let btnTransferir = document.querySelectorAll('.btn-transf');

        btnTransferir.forEach(btn => {
            btn.addEventListener("click", async () => {
                transferir(btn)
            });
        });

        fullName = allInfoAccount.FullNameCustomer
        document.getElementById("customerName").innerHTML = fullName

    })

    document.getElementById("side-transf").addEventListener("click", () => {
        redirectTo("transactions")
    })

    document.getElementById("btn-logout").addEventListener("click", () => {
        cleanSS()
        cleanLS()
        redirectTo("home")
    })

    cargarHistorial(clientAccounts[0].AccountId)
}

export const transferir = (btn) => {
    loadIntoSS("accountId", btn.parentNode.id)
    redirectTo("transactions")
}

export const cargarHistorial = async (accountId) => {

    let response = await getAllHistory(accountId)

    let sectionHistory = document.getElementById("movementHistory-section")

    let count = 0

    response.Data.data.map(async x => {

        count++

        if (count > 6)
            return

        let nameEmisor = x.fullNameEmisorCustomer
        let nameReceiver = x.fullNameReceiverCustomer
        let currency = x.currency
        let amount = x.amountTransaction
        let dateTime = x.dateTimeTransaction
        let fromCbu = x.fromCbu
        let toCbu = x.toCbu
        let operationType = x.operationType
        let fromAccountId = x.fromAccountId

        let mov = `<div class="flex">`

        if (
            operationType == "TRANSFERENCIA ENTRE CUENTAS DE DIFERENTE TITULAR" &&
            fromAccountId == accountId) {
            mov += `<span class="text-gray-700 mt-1 flex" style="">para: ${nameReceiver}</span>`
            mov += `<span class="text-red-600 mt-1 flex font-bold" style = "margin-left:auto" >- ${ currency }$ ${ amount }</span >`
        }else{
            mov += `<span class="text-gray-700 mt-1 flex" style="">de: ${nameEmisor}</span>`
            mov += `<span class="text-green-600 mt-1 flex font-bold" style = "margin-left:auto" > ${ currency }$ ${ amount }</span >`
        }
        
        mov += `</div ><hr class="mt-1 mb-2">`

        sectionHistory.insertAdjacentHTML("beforeend", mov)

    })

}