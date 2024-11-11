//H1
let h1 = document.querySelector("h1");
//MODALE
let modalWarper = document.getElementById("modalWrapper");
//FORM
let updateId=0;
let name=document.getElementById("name");
let director=document.getElementById("director");
let relaseDate=document.getElementById("relaseDate");
let img=document.getElementById("imgUrl");
//CATEGORIES
let categoriesSelect=document.getElementById("categories");
//TAGS
let tagsSelect=document.getElementById("tags");
//FULL TEXT SEARCH
let fullTextSearchInput=document.getElementById("fullTextSearch");
let btnSearch=document.getElementById("btnSearch");
//SECTION
let addFilmSection = document.getElementById("AddFilm");
let addFilmSectionCheck=false;
let UpdateOnView=false;
let filmWrapperSection = document.getElementById("FilmWrapper");
let filmListWrapper=document.querySelector(".film-list-custom");
let filmWrapperSectionCheck=false;
//BTN
let btnAddFilm = document.getElementById("Add");
let btnFilmList = document.getElementById("FilmList");
let btnLightTheme = document.getElementById("LightTheme");
let btnDarkTheme = document.getElementById("DarkTheme");
let btnUpdate = document.getElementById("Update");
let btnAddFilmForm=document.getElementById("addFilm");


//VALIDATION
function validation(film){
let error="";

if(film.filmName==""){
    error+="Inserisci il nome del film \n";
}

if(film.filmDirector==""){
    error+="Inserisci il regista del film \n";
}

if(film.filmRelaseDate==""){
    error+="Inserisci la data di uscita del film \n";
}

if(film.filmUrlImg==""){
    error+="Inserisci l'url della copertina \n";
}


if(error!=""){
    return error;
}else{
    return film;
}

}



//THEME LIGHT-DARK MODE
if (!localStorage.getItem("Theme")) {
    localStorage.setItem("Theme", JSON.stringify(true));
}


//THEME MODE
function lightTheme(){

    h1.classList.remove("text-white");
    document.body.classList.remove("bg-dark");
    document.body.classList.add("bg-light");
    localStorage.setItem("Theme", JSON.stringify(true));
    btnDarkTheme.classList.remove("d-none");
    btnLightTheme.classList.add("d-none");
    
}
function darkTheme(){
    
    h1.classList.add("text-white");
    document.body.classList.remove("bg-light");
    document.body.classList.add("bg-dark");
    localStorage.setItem("Theme", JSON.stringify(false));
    btnDarkTheme.classList.add("d-none");
    btnLightTheme.classList.remove("d-none");
    
}
window.onload = () => {

    if(localStorage.getItem("Theme") == "true"){
        lightTheme();
    }else if(localStorage.getItem("Theme") == "false"){
        darkTheme();
    }
    
}




// SHOW FILM LIST SECTION
function showFilmList(){
    indexFilms();
    if(!filmWrapperSectionCheck){
        filmWrapperSection.classList.remove("d-none");
        addFilmSection.classList.add("d-none");
        addFilmSectionCheck=false;
        filmWrapperSectionCheck=true;
    }else{
        filmWrapperSection.classList.add("d-none");
        filmWrapperSectionCheck=false;
    }

}




//INDEX FILMS
function indexFilms(){
    console.log("index");
    filmListWrapper.innerHTML="";
    fetch('https://localhost:7178/FilmsControllerSqlServe/Index').then(response => {if (!response.ok) {throw new Error('Network response was not ok ' + response.statusText);}return response.json();}).then(data => { 
    data.forEach(film => {
        let tags = "";
    
        Promise.all(film.filmsTagsPivots.map(tag => {
        return getTag(tag.filmTagTagId).then(tagg => {
            
            return `<span class=" text-white border border-black bg-warning rounded-3 px-1 me-1 mb-1 ">${tagg.tagName}</span>`;
        });
        })).then(results => {
       
        tags = results.join('');
       
        let div=document.createElement("div");
        div.classList.add("col-2","card","mx-4","my-4",);
        div.innerHTML=`<img src="${film.filmUrlImg}" class="card-img-top mt-2" alt="...">
                       <div class="card-body">
                            <h5 class="card-title">${film.filmName}</h5>
                            <p class="card-text">${film.filmDirector}</p>
                            <p class="card-text">${film.filmRelaseDate}</p>
                            <div class="mb-1">
                               <span class="card-text text-white border border-black bg-black rounded-3 px-1 pb-1">${film.filmCategory.categoryName}</span>
                            </div>
                            <div class="d-flex flex-wrap">
                            ${tags}
                            </div>
                            <div class="d-flex justify-content-start mt-3">
                                <button class="btn btn-primary me-1" onclick="infoFilm(${film.filmId})" data-bs-toggle="modal" data-bs-target="#modal${film.filmId}"><i class="fa-solid fa-eye"></i></button>
                                <button class="btn btn-danger me-1" onclick="deleteFilm(${film.filmId})" ><i class="fa-solid fa-trash"></i></button>
                                <button class="btn btn-warning" onclick="editFilm(${film.filmId})" ><i class="fa-solid fa-pen"></i></button>
                            </div>
                       </div>` 
        filmListWrapper.appendChild(div);
    });
    
    
    });   
    }).catch(error => {console.error('There was a problem with the fetch operation:', error);});
}




function FilmFullTextSeach(){
   
    let string=fullTextSearchInput.value;
    filmListWrapper.innerHTML="";
    fetch('https://localhost:7178/FilmsControllerSqlServe/Index').then(response => {if (!response.ok) {throw new Error('Network response was not ok ' + response.statusText);}return response.json();}).then(data => { 
        data.filter(film => film.filmName.toLowerCase().includes(string.toLowerCase())).forEach(film => {
            let tags = "";
        
            Promise.all(film.filmsTagsPivots.map(tag => {
            return getTag(tag.filmTagTagId).then(tagg => {
                
                return `<span class=" text-white border border-black bg-warning rounded-3 px-1 me-1 mb-1 ">${tagg.tagName}</span>`;
            });
            })).then(results => {
           
            tags = results.join('');
           
            let div=document.createElement("div");
            div.classList.add("col-2","card","mx-4","my-4",);
            div.innerHTML=`<img src="${film.filmUrlImg}" class="card-img-top mt-2" alt="...">
                           <div class="card-body">
                                <h5 class="card-title">${film.filmName}</h5>
                                <p class="card-text">${film.filmDirector}</p>
                                <p class="card-text">${film.filmRelaseDate}</p>
                                <div class="mb-1">
                                   <span class="card-text text-white border border-black bg-black rounded-3 px-1 pb-1">${film.filmCategory.categoryName}</span>
                                </div>
                                ${tags}
                                <div class="d-flex justify-content-start mt-3">
                                    <button class="btn btn-primary me-1" onclick="infoFilm(${film.filmId})" data-bs-toggle="modal" data-bs-target="#modal${film.filmId}"><i class="fa-solid fa-eye"></i></button>
                                    <button class="btn btn-danger me-1" onclick="deleteFilm(${film.filmId})" ><i class="fa-solid fa-trash"></i></button>
                                    <button class="btn btn-warning" onclick="editFilm(${film.filmId})" ><i class="fa-solid fa-pen"></i></button>
                                </div>
                           </div>` 
            filmListWrapper.appendChild(div);
        });});
    }).catch(error => {console.error('There was a problem with the fetch operation:', error);});
}





//CATEGORIES
fetch('https://localhost:7178/Categories/Index').then(response => {if (!response.ok) {throw new Error('Network response was not ok ' + response.statusText);}return response.json();}).then(data => { 
    data.forEach(categorie => {
        categoriesSelect.innerHTML+=`<option value="${categorie.categoryId}">${categorie.categoryName}</option>`
    });   
}).catch(error => {console.error('There was a problem with the fetch operation:', error);});

function getCategory(id){
    fetch('https://localhost:7178/Categories/Details/'+id+'').then(response => {if (!response.ok) {throw new Error('Network response was not ok ' + response.statusText);}return response.json();}).then(data => { 
       return data  
    }).catch(error => {console.error('There was a problem with the fetch operation:', error);});
}




//TAGS
fetch('https://localhost:7178/Tags/Index').then(response => {if (!response.ok) {throw new Error('Network response was not ok ' + response.statusText);}return response.json();}).then(data => {
    data.forEach(tag => {
        tagsSelect.innerHTML+=`<option value="${tag.tagId}">${tag.tagName}</option>`
    });
})
function getTag(id) {
    return fetch('https://localhost:7178/Tags/Details/' + id)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.json();
        });
}



//FILM UD-INFO
function infoFilm(id){
    modalWarper.innerHTML="";
    let url = 'https://localhost:7178/FilmsControllerSqlServe/Details/' + id;
    fetch(url).then(response => {if (!response.ok) {throw new Error('Network response was not ok ' + response.statusText);}return response.json();}).then(film => {
    
        Promise.all(film.filmsTagsPivots.map(tag => {
            return getTag(tag.filmTagTagId).then(tagg => {
                
                return `<span class=" text-white border border-black bg-warning rounded-3 px-1 me-1 mb-1 ">${tagg.tagName}</span>`;
            });
            })).then(results => {
           
        tags = results.join('');
    
        modalWarper.innerHTML =` 
                <div class="modal fade" id="modal${film.filmId}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog">
                  <div class="modal-content">
                    <div class="modal-header">
                      <h1 class="modal-title fs-5" id="staticBackdropLabel">Informazioni Film</h1>
                      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <img src="${film.filmUrlImg}" class="card-img-top mt-2" alt="...">
                   <div class="card-body">
                        <h5 class="card-title">${film.filmName}</h5>
                        <p class="card-text">${film.filmDirector}</p>
                        <p class="card-text">${film.filmRelaseDate}</p>
                        <div class="mb-1">
                           <span class="card-text text-white border border-black bg-black rounded-3 px-1 pb-1">${film.filmCategory.categoryName}</span>
                        </div>
                        ${tags}
                        <div class="d-flex justify-content-start mt-3">
                            <button class="btn btn-primary me-1" onclick="infoFilm(${film.filmId})" data-bs-toggle="modal" data-bs-target="#modal${film.filmId}"><i class="fa-solid fa-eye"></i></button>
                            <button class="btn btn-danger me-1" onclick="deleteFilm(${film.filmId})" ><i class="fa-solid fa-trash"></i></button>
                            <button class="btn btn-warning" onclick="editFilm(${film.filmId})" ><i class="fa-solid fa-pen"></i></button>
                        </div>
                   </div>
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                  </div>
                </div>
              </div>
              `
            new bootstrap.Modal(document.getElementById(`modal${film.filmId}`)).show();
        })     
    }).catch(error => {console.error('There was a problem with the fetch operation:', error);});
   
    
}
function deleteFilm(id){
    let url = 'https://localhost:7178/FilmsControllerSqlServe/Delete/' + id;
    fetch(url, {
        method: 'DELETE', 
        headers: {
          'Content-Type': 'application/json', 
          
        }
      }).then(response => {

          if (!response.ok) {
            throw new Error('Request failed with status ' + response.status);
          }
          return response.json();

      }).then(data => {

          console.log('Deleted successfully:', data); 

      }).catch(error => {

          console.error('Error:', error);

    });  
    window.location.reload();
    
}
function editFilm(id){
    UpdateOnView=true;
    showAddFilmForm();
    UpdateOnView=false;
    
    let url = 'https://localhost:7178/FilmsControllerSqlServe/Details/' + id;
    fetch(url).then(response => {if (!response.ok) {throw new Error('Network response was not ok ' + response.statusText);}return response.json();}).then(film => {
        updateId=film.filmId;
        name.value = film.filmName;
        director.value = film.filmDirector;
        relaseDate.value = film.filmRelaseDate;
        img.value = film.filmUrlImg;
        categoriesSelect.value = film.filmCategoryId;
        
    }).catch(error => {console.error('There was a problem with the fetch operation:', error);});
    
}

function updateFilm(){    
    let film={
        filmName:name.value,
        filmDirector: director.value,
        filmRelaseDate: relaseDate.value,
        filmUrlImg: img.value,
        filmCategoryId: categoriesSelect.value}
    
    
    if(validation(film)==film){
        let url = 'https://localhost:7178/FilmsControllerSqlServe/Update/' + updateId;
        fetch(url, {
        method: 'PUT', 
        headers: {
          'Content-Type': 'application/json', 
          
        },
        body: JSON.stringify(film)
      }).then(response => {

          if (!response.ok) {
            throw new Error('Request failed with status ' + response.status);
          }
          return response.json();

      }).then(data => {
          console.log('Updated successfully:', data); 

      }).catch(error => {

          console.error('Error:', error);   
      }
    );  
    window.location.reload();

    }else{
        alert(validation(film));
    }
    
    
}




// SHOW ADD FILM SECTION
function showAddFilmForm(){
    if(UpdateOnView){

        if(!addFilmSectionCheck){
            addFilmSection.classList.remove("d-none");
            filmWrapperSection.classList.add("d-none");
            btnUpdate.classList.remove("d-none");
            btnAddFilmForm.classList.add("d-none");
            filmWrapperSectionCheck=false;
            addFilmSectionCheck=true;
        }else{
            addFilmSection.classList.add("d-none");
            addFilmSectionCheck=false;
        }

    }else{
        resetForm();
        if(!addFilmSectionCheck){
            addFilmSection.classList.remove("d-none");
            filmWrapperSection.classList.add("d-none");
            btnUpdate.classList.add("d-none");
            btnAddFilmForm.classList.remove("d-none");
            filmWrapperSectionCheck=false;
            addFilmSectionCheck=true;
        }else{
            addFilmSection.classList.add("d-none");
            addFilmSectionCheck=false;
        }
    }
}

function resetForm() {
    name.value = "";
    director.value = "";
    relaseDate.value = "";
    img.value = "";
}
 
function addFilm(){
    
   
    
    film={
        filmName: name.value,
        filmDirector: director.value,
        filmRelaseDate: relaseDate.value,
        filmUrlImg: img.value,
        filmCategoryId: categoriesSelect.value,
       
    }

    console.log(film);

    if(validation(film)==film){
        let url = 'https://localhost:7178/FilmsControllerSqlServe/Add';
        fetch(url, {
            method: 'POST', 
            headers: {
              'Content-Type': 'application/json', 
              
            },
            body: JSON.stringify(film)
          }).then(response => {
    
              if (!response.ok) {   
                throw new Error('Request failed with status ' + response.status);
              }
              return response.json();
    
          }).then(data => {
    
              console.log('Added successfully:', data); 
    
          }).catch(error => {
    
              console.error('Error:', error);
    
        });  
        resetForm();
        window.location.reload();
        

    }else{
        alert(validation(film));
    }
    
}
   
