function CardMovement(params) {
    let tbodyElments = document.getElementById('movementHistory-section')
    tbodyElments.innerHTML = ''
    if (tbodyElments != null && params != null) {
        params.forEach(element => {
            tbodyElments.innerHTML += `
            <div class="movement-card" id="movement-card">         
            <p class ="infoMovement">${element.info}</p>
            <p class = "amountMovement">${element.amount}</p>
        </div>  `
        })
    }
}

export { CardMovement }