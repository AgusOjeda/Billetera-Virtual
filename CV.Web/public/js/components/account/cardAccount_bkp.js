function Card(params) {
    let tbodyElments = document.getElementById('root')
    tbodyElments.innerHTML = ''
    if (tbodyElments != null && params != null) {
        params.forEach(element => {
            tbodyElments.innerHTML += `
            <div class="shop-card" id="card-shop">
                    
            <div class="title">
            <p class ="balance">BALANCE</p>
            <p class = "currency"> $ ${element.Currency} ${element.Balance}</p>
            <button class="details" id="${element.id}" >
              Details<span class="bg"></span>
            </button>
            </div>
            
            <div class="cta">
                <p class="cbu">${element.Cbu} </p>
                <button class="transaction" onClick="window.AddToCart()">
                    Transaction<span class="bg"></span>
                </button>
            </div>
        </div>  `
    })

    let detailsButton = document.querySelectorAll('.details')

        for (var i = 0; i < detailsButton.length; i++){
            detailsButton[i].addEventListener('click', DetailOfAccount)
            detailsButton[i].myParam = detailsButton[i].id
        }        
    }
    function DetailOfAccount(evt) {
        let accountId = evt.currentTarget.myParam
        window.DetailOfAccount(accountId)
    }
}

export { Card }