# Laboratorul 2
---

## Întrebări și Răspunsuri

### 1. Ce este un viewport?
Un **viewport** este o secțiune a ferestrei în care OpenGL desenează scena 3D sau 2D. De obicei, ocupă întreaga fereastră, dar poate fi configurat să ocupe doar o parte din aceasta.

### 2. Ce reprezintă conceptul de frames per second (FPS) din punctul de vedere al bibliotecii OpenGL?
**FPS** (Frames per Second) reprezintă numărul de cadre (imagini) pe care OpenGL le randă pe secundă. Un FPS mai mare înseamnă o animație mai fluidă și o performanță mai bună.

### 3. Când este rulată metoda `OnUpdateFrame()`?
Metoda `OnUpdateFrame()` este apelată la fiecare cadru și se ocupă de actualizarea logicii de joc sau a mișcării obiectelor, înainte de a fi randate.

### 4. Ce este modul imediat de randare?
**Modul imediat** de randare în OpenGL este o metodă veche de a desena obiecte, în care comenzile de randare sunt trimise direct către GPU pentru fiecare cadru. Aceasta metodă este mai puțin eficientă comparativ cu tehnicile moderne de randare bazate pe *Vertex Buffer Objects* (VBO-uri).

### 5. Care este ultima versiune de OpenGL care acceptă modul imediat?
Modul imediat a fost depreciat începând cu **OpenGL 3.0** și complet eliminat în **OpenGL 3.2**.

### 6. Când este rulată metoda `OnRenderFrame()`?
Metoda `OnRenderFrame()` este apelată la fiecare cadru, după `OnUpdateFrame()`. În această metodă are loc desenarea (randarea) obiectelor pe ecran.

### 7. De ce este nevoie ca metoda `OnResize()` să fie executată cel puțin o dată?
Metoda `OnResize()` este necesară pentru a ajusta dimensiunea viewport-ului și aspect ratio-ul (raportul dintre lățimea și înălțimea ferestrei) atunci când fereastra este redimensionată, asigurându-se astfel că scena 3D rămâne proporțională.

### 8. Ce reprezintă parametrii metodei `CreatePerspectiveFieldOfView()` și care este domeniul de valori pentru aceștia?
Metoda `CreatePerspectiveFieldOfView()` definește un câmp de vedere pentru proiecția perspectivală. Parametrii includ:
- **FOV (Field of View)**: Unghiul de vizualizare vertical, exprimat în radiani. De obicei, valoarea sa se află între 45 și 90 de grade.
- **Aspect Ratio**: Raportul dintre lățimea și înălțimea viewport-ului, care trebuie să fie un număr pozitiv.
- **Near și Far**: Distanțele de aproape și de departe pentru vizualizarea scenelor.

