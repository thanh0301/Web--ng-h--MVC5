function getEle(id){
    return  document.getElementById(id);
  }

var danhSachDiem = []

var tbody =getEle("tblBody");

// var scores= tbody.rows[0].cells[3].innerHTML;
// var scores= tbody.rows[1].cells[3].innerHTML;
// var scores= tbody.rows[2].cells[3].innerHTML;
// var scores= tbody.rows[3].cells[3].innerHTML;
// var scores= tbody.rows[4].cells[3].innerHTML;
// var scores= tbody.rows[5].cells[3].innerHTML;
// var scores= tbody.rows[6].cells[3].innerHTML;

function layDanhSachDiem(){

    for(var i= 0; i < tbody.rows.length;i++){

        var dtb= tbody.rows[i].cells[3].innerHTML * 1;

        danhSachDiem.push(dtb)
    }
    console.log(danhSachDiem);
}


layDanhSachDiem();

getEle("btnSVCaoDiemNhat").addEventListener("click",function(){

    var max =danhSachDiem[0] ;
    var index =0;
    for(var i=1;i<danhSachDiem.length;i++){
       if(max<danhSachDiem[i]){
            max = danhSachDiem[i];
            index=i;
       }
           
    }


    var ten =tbody.rows[index].cells[2].innerHTML;
    var kq ="Tên: "+ten + " - " + max;

    getEle("svGioiNhat").innerHTML=kq;
});

getEle("btnSVThapDiemNhat").addEventListener("click",function(){

    var min =danhSachDiem[0] ;
    var index =0;
    for(var i=1;i<danhSachDiem.length;i++){
       if(min>danhSachDiem[i]){
        min = danhSachDiem[i];
            index=i;
       }
           
    }


    var ten =tbody.rows[index].cells[2].innerHTML;
    var kq ="Tên: "+ten + " - " + min;

    getEle("svYeuNhat").innerHTML=kq;
});


function xeploai(dtb){

    if(dtb>= 8.5){
        console.log("Giỏi");
    }
    else if(dtb >=7.5){
        console.log("Khá");
    }
    else if(dtb >=5){
        console.log("yếu");
    }

}

getEle("btnSoSVGioi").addEventListener("click",function(){

   
})


