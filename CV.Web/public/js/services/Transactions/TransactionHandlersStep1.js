import { getAccountByCbu } from "./AccountForTransactionServices.js"
import { getAccountByAlias } from "./AccountForTransactionServices.js"
import { getAccountByAccountId } from "./AccountForTransactionServices.js"
import { loadIntoSS } from "../SessionStorageService.js"
import { cleanLS } from "../LocalStorageService.js"
import { cleanSS } from "../SessionStorageService.js"
import { redirectTo } from "../RedirectService.js"

export const initHandlersStep1 = () => {

    const btnContinue = document.getElementById('step1-continuar-transf');

    btnContinue.addEventListener("click", async () => {

        let amount = document.getElementById("origen-monto").value
        let reason = document.getElementById("motivo").value
        let receiverCbuOrAlias = document.getElementById("cbu-alias-destino").value
        let balance = document.getElementById("disponible-cuenta").value.substring(5);

        // AGREGAR MAS COMPROBACIONES!!!!! ----<<<<<<<

        if (amount > 0 && reason != "" && receiverCbuOrAlias != "" && amount <= parseInt(balance)) {
            continueNextStep(amount, reason, receiverCbuOrAlias)
        } else {
            alert("Por favor revise los datos de entrada!!")
        }

    })

    let selectFromAccount = document.getElementById("cuenta-origen")

    selectFromAccount.addEventListener("change", async (event) => {
        let accountId = event.target.value
        loadIntoSS("accountId", accountId)
        let allInfoAccount = await getAccountByAccountId(accountId)
        let inputBalance = document.getElementById("disponible-cuenta")
        inputBalance.value = `${allInfoAccount.Data.Currency}$ ` + allInfoAccount.Data.Balance
    })

    document.getElementById("side-home").addEventListener("click", () => {
        redirectTo("home")
    })

    document.getElementById("btn-logout").addEventListener("click", () => {
        cleanSS()
        cleanLS()
        redirectTo("home")
    })
}

const continueNextStep = async (amount, reason, receiverCbuOrAlias) => {

    let emisorAccount = document.getElementById("cuenta-origen").value

    let toAccountId

    if (document.getElementById("destino-is-cbu").checked) {
        toAccountId = await getReceiverAccountByCbu(receiverCbuOrAlias)
    } else {
        toAccountId = await getReceiverAccountByAlias(receiverCbuOrAlias)
    }

    let transaction = {
        fromAccountId: emisorAccount,
        toAccountId: toAccountId,
        operationType: 2,
        reason: reason,
        amount: amount
    }

    loadIntoSS("step-1-trx", transaction)
    loadIntoSS("toAliasOrCbu", receiverCbuOrAlias)

    redirectTo("transactions-step-2")
}

const getReceiverAccountByCbu = async (cbu) => {
    let response = await getAccountByCbu(cbu)
    return response.Data.Data.AccountId
}

const getReceiverAccountByAlias = async (alias) => {
    let response = await getAccountByAlias(alias)
    return response.Data.Data.AccountId
}