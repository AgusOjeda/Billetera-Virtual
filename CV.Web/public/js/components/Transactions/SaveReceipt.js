const btnSave=document.getElementById("btnSave")
const SaveAsPdf=()=>{
    var data = document.getElementById('comprobante');
    html2canvas(data,{scale: 2}).then(canvas => {

        var imgData = canvas.toDataURL('image/JPEG');
        var imgWidth = 210;
        var pageHeight = 295;
        var imgHeight = canvas.height * imgWidth / canvas.width;
        var heightLeft = imgHeight;

        var doc = new jsPDF('p', 'mm', "a4");
        var position = 1;

        doc.addImage(imgData, 'JPEG', 0, position, imgWidth, imgHeight,'FAST');
        heightLeft -= pageHeight;

        while (heightLeft >= 0) {
            position = heightLeft - imgHeight;
            doc.addPage();
            doc.addImage(imgData, 'JPEG', 0, position, imgWidth, imgHeight);
            heightLeft -= pageHeight;
        }
      doc.save("Comprobante.pdf");
      });
}
btnSave.addEventListener("click", SaveAsPdf)