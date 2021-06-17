# REST API – Organizačná štruktúra firmy
Projekt bol vytvorený vo Visual Studiu. 
  - *kros.OrganizáciaFirmy/database* obsahuje súbory na vytvorenie databáz alebo na vloženie do databáz
  - projekt obsahuje 4 databázy: dbo.ZoznamZamestnancov, dbo.ZoznamOddeleni, dbo.ZoznamProjektov a dbo.ZoznamDivizii
  - tie su herarchicky usporiadané: firma(všetci zamestnanci) → divízie → projekty → oddelenia
  - *kros.OrganizáciaFirmy/postman* obsahuje .json subor pre manipuláciu s databázami
  - pre použitie API klienta je potrebné zmeniť v Postmanovi *localhost:44381* na localhosta daného počítača

**Zamestnanci** controllers:
- *https://localhost:44381/firma/zamestnanci* ____ GET: vráti všetkých zamestnancov v databáze
- *https://localhost:44381/firma/zamestnanci/2* ____ GET: vráti zamestnanca s Id 2
- *https://localhost:44381/firma/zamestnanci/riaditelFirmy* ____ GET: vráti všetkých riaditeľov firmy
- *https://localhost:44381/firma/zamestnanci* ____ POST: pridá zamestnanca [FromBody]
- *https://localhost:44381/firma/zamestnanci/2* ____ PUT: upravi zamestnanca s id 2 [FromBody] 
- *https://localhost:44381/firma/zamestnanci/9* ____ DEL: vymaze zamestnanca s Id 9

**Divizie** controllers:
- *https://localhost:44381/firma/divizie* ____ GET: vráti všetky divízie firmy
- *https://localhost:44381/firma/divizie/4* ____ GET: vráti zamestnancov z divízie Id 4
- *https://localhost:44381/firma/divizie* ____ POST: pridá novo vytvorenú divíziu [FromBody]
- *https://localhost:44381/firma/divizie/1* ____ PUT: pridá zamestnanca do divízie Id 1 [FromForm]
- *https://localhost:44381/firma/divizie/4* ____ DELETE: vymaže zamestnanca z divízie Id 4 [FromForm]
- *https://localhost:44381/firma/divizie/10* ____ PUT: nastaví vedúceho zamestnanca divízie Id 10 [FromForm]
- *https://localhost:44381/firma/divizie/2/veduci* ____ GET: vráti vedúceho divízie s Id 2

**Projekty** controllers:
- *https://localhost:44381/firma/divizie/2/projekty* ____ GET: vráti všetky projekty z divízie Id 2
- *https://localhost:44381/firma/divizie/4/projekty/2/zamestnanciProjektu* ____ GET: vráti všetkých zamestnancov z divízie id 4, projekt Id 2
- *https://localhost:44381/firma/divizie/1/projekty/6* ____ PUT: pridá zamestnanca do projektu s id 6  [FromForm]
- *https://localhost:44381/firma/divizie/1/projekty/6* ____ DELETE: odstrani zamestnanca z projektu s id 6  [FromForm]

**Oddelenia** controllers:
- *https://localhost:44381/firma/divizie/2/projekty/7/oddelenia* ____ GET: vráti všetky oddelenia z projektu Id 7
- *https://localhost:44381/firma/divizie/4/projekty/2/oddelenia/3/zamestnanciOddelenia* ____ GET: vráti zamestnancov z oddelenia 3
- *https://localhost:44381/firma/divizie/1/projekty/6/oddelenia/6* ____ PUT: pridá zamestnancado oddelenia s id 6  [FromForm]
- *https://localhost:44381/firma/divizie/1/projekty/6/oddelenia/6* ____ DELETE: odstrani zamestnanca z projektu s id 6 [FromForm]
