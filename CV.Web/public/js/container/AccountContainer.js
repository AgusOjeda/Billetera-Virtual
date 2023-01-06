import { sideBar } from "../components/SideBar.js"
import { loadCards } from "../services/Account/LoadDataInAccountView.js"
import { mainCardList } from "../components/account/CardList.js"
import { CardMovement } from "../components/account/LastMovements.js"

let _main = document.getElementById("root")

export const InitAccount = async () => {
    _main.insertAdjacentHTML("beforeend", sideBar())
    _main.insertAdjacentHTML("beforeend", mainCardList)

    document.getElementById("side-home").classList.add("bg-bgPrimary", "text-bgSidebar")

    loadCards()

}