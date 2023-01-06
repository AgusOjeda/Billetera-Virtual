import { Account } from "./AccountInit.js"
import { Trx } from "./TransactionInit.js"
import { GetJwtToken } from "./services/LocalStorageService.js"
import { getParamFromURL } from "./services/URLGetService.js"

window.onload = async () => {
    
    let token = GetJwtToken()

    if(token == null){
        window.location.href = "landing.html"
    }

    let section = getParamFromURL("section")
    
    switch(section){
        default:
        case "home":
            Account()
            break;
        case "transactions":
        case "transactions-step-2":
        case "transactions-step-3":
        Trx()
            break;
    }
}


// import { GetAccountById, GetAccountsForCustomer, DeleteAccount, UpdateAlias, GetTransactionHistory } from "./services/AccountServices.js"
// import { Card} from "./components/account/cardAccount.js"
// import { AccountInfo} from "./models/AccountInfo.js"
// import { ModalAccount} from "./components/account/modalAccount.js"
// import { ModalAlias} from "./components/account/modalAlias.js"
// import { CardMovement} from "./components/account/LastMovements.js"
// import { CardMovementHistory} from "./models/CardMovementHistory.js"

// let token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4YzI3NjIyNy0yMGU1LTRiMGQtOTAzNC1lZTQ2YTlmN2U2ZmUiLCJlbWFpbCI6ImFyaWVsQGdtYWlsLmNvbSIsIm5iZiI6MTY2OTQ3MjI4NSwiZXhwIjoxNjY5NDc5NDg1LCJpYXQiOjE2Njk0NzIyODV9.hDRLfDXZS5uMchyVEh4CGcAEwfMb-1Mf29v705Qf4Ng"
    
// window.onload = async () => {
//     await GetAccountsCustomer()
//     SetTokenInSession('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4YzI3NjIyNy0yMGU1LTRiMGQtOTAzNC1lZTQ2YTlmN2U2ZmUiLCJlbWFpbCI6ImFyaWVsQGdtYWlsLmNvbSIsIm5iZiI6MTY2OTQ3MjI4NSwiZXhwIjoxNjY5NDc5NDg1LCJpYXQiOjE2Njk0NzIyODV9.hDRLfDXZS5uMchyVEh4CGcAEwfMb-1Mf29v705Qf4Ng')
//     ShowCardsAccount()
// }

// function SetTokenInSession(object) {
//     sessionStorage.setItem('token', JSON.stringify(object))
// }

// function GetTokenInSession() {
//     var json = sessionStorage.getItem('token')
//     let token = ""
//     if (json != null) {
//         token = JSON.parse(json)
//     }   
//     return token
// }

// function ShowCardsAccount(array) {
//     array = GetCardsInSession()
//     Card(array)
// }


// async function GetAccountsCustomer() {
//     let id = '4A92E7A6-4C1A-4838-9709-08DAB4F9DB21'
//     let arrayId = await ObtenerId(id)
//     let arrayCards = await ObtenerCards(arrayId)
//     SetCardsInSession(arrayCards)
// }

// async function ObtenerId(id) {
//     let arrayId = []
//     let tokenSession = GetTokenInSession()
//     const response = await GetAccountsForCustomer(id, tokenSession)
//     response.forEach(element => {
//         arrayId.push(element.AccountId)
//     });

//     SetIDCardsInSession(arrayId)
//     return arrayId;
// }

// async function ObtenerCards(array) {
//     let cardsCustomer = []    
//     let tokenSession = GetTokenInSession()

//     for (const id of array) {
//         let resp = await GetAccountById(id, tokenSession)
//         var accountInfo = new AccountInfo(id, resp.Alias, resp.Cbu, resp.Dni, resp.Balance, resp.Cuil, resp.Currency, resp.FullNameCustomer, resp.AccountState)
//         cardsCustomer.push(accountInfo)
//     }
//     return cardsCustomer;
// }

// async function AccountDelete() {
//     let tokenSession = GetTokenInSession()
//     let id= '05350FF1-F33F-4111-C830-08DACB395985'
//     const response = await DeleteAccount(id, tokenSession)
// }

// function SetCardsInSession(object) {
//     sessionStorage.setItem('cards', JSON.stringify(object))
// }

// function GetCardsInSession() {
//     var json = sessionStorage.getItem('cards')
//     let cards = []
//     if (json != null) {
//         cards = JSON.parse(json)
//     }   
//     return cards
// }

// function SetIDCardsInSession(object) {
//     sessionStorage.setItem('IDcards', JSON.stringify(object))
// }

// function GetIDCardsInSession() {
//     var json = sessionStorage.getItem('IDcards')
//     let cardsID = []
//     if (json != null) {
//         cardsID = JSON.parse(json)
//     }   
//     return cardsID
// }


// // MODAL
// window.DetailOfAccount = async (id) => {
//     let tokenSession = GetTokenInSession()
//     let resp = await GetAccountById(id, tokenSession)
//     let cuil = resp.Cuil
//     let cuilSplit = cuil.split('-')
//     let newCuil = cuilSplit[0] + cuilSplit[1] + cuilSplit[2] 
//     var accountInfo = new AccountInfo(id, resp.Alias, resp.Cbu, resp.Dni, resp.Balance, newCuil, resp.Currency, resp.FullNameCustomer, resp.AccountState)
//     var storedAccount = {}
//     storedAccount = accountInfo
//     sessionStorage.setItem('account', JSON.stringify(storedAccount))
//     ModalAccount(storedAccount)
// }

// window.ChangeAlias = async (id) => {
//     let tokenSession = GetTokenInSession()
//     let account = await GetAccountById(id, tokenSession)
//     ModalAlias(account.Alias, id)
// }

// window.SetAlias = async (id, alias) => {
//     let tokenSession = GetTokenInSession()
//     let resp = await UpdateAlias(id, alias, tokenSession)
//     return resp
// }


// //CARDMOVEMENT
// async function GetHistoryTransaccionsByAccountId() {
//     let arrayIDCards = GetIDCardsInSession()
//     let arrayHistoryAllAccount = []
//     let tokenSession = GetTokenInSession()
    
//     for(const i of arrayIDCards){
//         const resp = await GetTransactionHistory(i, tokenSession)

//         if (resp != null) {
//             let arrayHistoryEachAccount = MovementFilterByState(resp)
//             arrayHistoryAllAccount.push(arrayHistoryEachAccount)
//         }
//     } 

//     SetHistoryAllAccountInSession(arrayHistoryAllAccount)
//     ShowCardsMovement()
// }


// function MovementFilterByState(array) {
//     let arrayHistoryEachAccount = []
    
//     array.forEach(element => {
//         if (element.resultingStateOfTransaction = "TRX SUCCESS") {
//             arrayHistoryEachAccount.push(element)
//         }
//     });
//     return arrayHistoryEachAccount;
// }

// function SetHistoryAllAccountInSession(object) {
//     sessionStorage.setItem('historyAllAccounts', JSON.stringify(object))
// }

// function GetHistoryAllAccountInSession() {
//     var json = sessionStorage.getItem('historyAllAccounts')
//     let accountHistory = []
//     if (json != null) {
//         accountHistory = JSON.parse(json)
//     }   
//     return accountHistory
// }


// function ShowCardsMovement() {
//     let arrayDTOMovement = []

//     let arrayHistory = GetHistoryAllAccountInSession()
//     arrayHistory.forEach(element => {
//         element.forEach(el => {
//             if (el.operationType == "INGRESO DE DINERO POR VENTANILLA") {
//                 let am = "+"+el.currency + " " + el.amountTransaction
//                 let cardMovement = new CardMovementHistory("Depósito de dinero", am)
//                 arrayDTOMovement.push(cardMovement)
//             }
//             if (el.operationType == "EXTRACCION DE DINERO POR VENTANILLA") {
//                 let am = "-"+el.currency + " " + el.amountTransaction
//                 let cardMovement = new CardMovementHistory("Extracción de dinero", el.Currency + am)
//                 arrayDTOMovement.push(cardMovement)
//             }
//             if (el.operationType == "TRANSFERENCIA ENTRE CUENTAS DE MISMO TITULAR") {
//                 let vav = "a cbu: " + el.toCbu
//                 let am = el.currency + " " + el.amountTransaction
//                 let cardMovement = new CardMovementHistory(vav, am)
//                 arrayDTOMovement.push(cardMovement)
//             }
//             if (el.operationType == "TRANSFERENCIA ENTRE CUENTAS DE DIFERENTE TITULAR") {  
//                 let vav = "para: " + el.fullNameReceiverCustomer
//                 let am = "-"+el.currency + " " + el.amountTransaction
//                 let cardMovement = new CardMovementHistory(vav, am)
//                 arrayDTOMovement.push(cardMovement)
//             }
//        })
   
//     });

//     let arraySevenMov = arrayDTOMovement.slice(0,6)

//     CardMovement(arraySevenMov)
// } 

    
// function SetHistoryAccountInSession(object) {
//     sessionStorage.setItem('historyAccounts', JSON.stringify(object))
// }

// function GetHistoryAccountInSession() {
//     var json = sessionStorage.getItem('historyAccounts')
//     let accountHistory = []
//     if (json != null) {
//         accountHistory = JSON.parse(json)
//     }   
//     return accountHistory
// }

// function TableHistory() {
//     let arrayHistoryAccount = GetHistoryAccountInSession()
// }

// function ShowTable() {
//     let tbodyElments = document.getElementById('history-table-id')
//     var accountHistory = GetHistoryAccountInSession()
//     if (tbodyElments != null && accountHistory != null) {
//         tbodyElments.innerHTML = ''
//         accountHistory.forEach(b => {
//             let dateSale = b.DateOperation
//             let dateSplit = dateSale.split('T')
//             tbodyElments.innerHTML += ` 
//           <tr>
//             <td class="table-row" ><p class="table-data"> ${b.FromName} </p></td>
//             <td class="table-row" ><p class="table-data"> ${b.FromCbu} </p></td>
//             <td class="table-row" ><p class="table-data"> ${b.ToName} </p></td>
//             <td class="table-row" ><p class="table-data"> ${b.ToCbu} </p></td>
//             <td class="table-row" ><p class="table-data">$ ${b.Amount} </p></td>
//             <td class="table-row" ><p class="table-data">$ ${b.TypeOperation} </p></td>
//             <td class="table-row" ><p class="table-data"> ${dateSplit[0]} </p></td>
//             <td class="table-row" ><p class="table-data">$ ${b.StateOperation} </p></td>
//             </td>     
//         </tr>       
//     `
//         }
//         )
//     }
// }
