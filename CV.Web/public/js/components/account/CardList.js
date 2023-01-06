export const mainCardList = `
        <main class="grid grid-auto-rows col-start-3 col-end-11">
            <cards class="grid grid-cols-12 place-self-center gap-2 w-full" id="cards-account">
            </cards>
            <div class="grid grid-cols-2 grid-flow-col auto-cols-max place-self-center gap-2" id="chartDiv">
            <div class="col-start-1 rounded-lg bg-white shadow-sm p-5 m-5" id="graph">
            <img src="../images/graph.png">
                <!--<canvas id="myChart"></canvas>-->
                <div class="grid grid-cols-3 text-center mt-4">
                    <div class="text-sm">
                        <p>Transferencias</p>
                        <p class="text-blue-700 text-3xl">20%</p>
                    </div>
                    <div class="text-sm">
                        <p>Extracciones</p>
                        <p class="text-gold text-3xl">23%</p>
                    </div>
                    <div class="text-sm">
                        <p>Depositos</p>
                        <p class="text-gray-500 text-3xl">57%</p>
                    </div>
                </div>
                
                </div>
                <div class="text-left col-start-2 rounded-lg bg-white shadow-sm p-3 m-5" id="movementHistory-section">
                    <p class="font-bold text-gray-700 mb-3">Ultimos movimientos</p>
                </div>
            </div>
        </main>
`