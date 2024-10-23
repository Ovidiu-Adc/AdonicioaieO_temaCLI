# Laborator 03 

## 1. Care este ordinea de desenare a vertexurilor pentru aceste metode (orar sau anti-orar)?

Ordinea de desenare a vertexurilor în OpenGL este **anti-orară**. Aceasta este ordinea folosită implicit pentru a determina că fața unui poligon este orientată spre utilizator. Dacă vertexurile sunt desenate în ordine orară, fața va fi considerată orientată în spate și nu va fi vizibilă (dacă este activat culling-ul).

### Cod pentru desenarea axelor de coordonate:
```csharp
GL.Begin(PrimitiveType.Lines);
// Axe X (Roșu)
GL.Color3(1.0f, 0.0f, 0.0f);  // Roșu
GL.Vertex3(-10.0f, 0.0f, 0.0f);
GL.Vertex3(10.0f, 0.0f, 0.0f);

// Axe Y (Verde)
GL.Color3(0.0f, 1.0f, 0.0f);  // Verde
GL.Vertex3(0.0f, -10.0f, 0.0f);
GL.Vertex3(0.0f, 10.0f, 0.0f);

// Axe Z (Albastru)
GL.Color3(0.0f, 0.0f, 1.0f);  // Albastru
GL.Vertex3(0.0f, 0.0f, -10.0f);
GL.Vertex3(0.0f, 0.0f, 10.0f);
GL.End();
```

## 2. Ce este anti-aliasing?

**Anti-aliasing** este o tehnică utilizată pentru a netezi marginile zimțate care apar atunci când se desenează forme diagonale sau curbe pe o grilă de pixeli. Aceasta ajută la estomparea treptată a culorilor la margini, oferind un aspect mai fluid și mai natural.


## 3. Care este efectul rulării comenzii `GL.LineWidth(float)`? Dar pentru `GL.PointSize(float)`? Funcționează în interiorul unei zone `GL.Begin()`?

- **`GL.LineWidth(float)`** setează grosimea liniilor desenate. Liniile vor deveni mai groase în funcție de valoarea specificată.
- **`GL.PointSize(float)`** setează dimensiunea punctelor, ceea ce face ca punctele să apară mai mari sau mai mici pe ecran.

Aceste comenzi **nu funcționează** în interiorul unei zone `GL.Begin()`. Ele trebuie apelate înainte de începerea desenării pentru a avea efect.


## 4. Răspundeți la următoarele întrebări:

- **Care este efectul utilizării directivei `LineLoop`?**
  `LineLoop` creează o serie de linii conectate și închide bucla prin desenarea unei linii de la ultimul vertex înapoi la primul.

- **Care este efectul utilizării directivei `LineStrip`?**
  `LineStrip` creează o serie de linii conectate, dar nu închide bucla. Nu se desenează o linie de la ultimul vertex la primul.

- **Care este efectul utilizării directivei `TriangleFan`?**
  `TriangleFan` creează o serie de triunghiuri conectate printr-un punct central comun, formând un ventilator de triunghiuri.

- **Care este efectul utilizării directivei `TriangleStrip`?**
  `TriangleStrip` creează o bandă de triunghiuri conectate, fiecare triunghi împărțind două vertexuri cu triunghiul anterior.


## 5. Creați un proiect elementar (OpenGL_conn_ImmediateMode).

În acest subpunct, trebuie să creați un proiect OpenGL în care să implementați modul imediat de randare și să configurați corect viewport-ul. Proiectul poate fi numit sugestiv **ImmediateModeExample**.


## 6. De ce este importantă utilizarea culorilor diferite la desenarea obiectelor 3D?

Utilizarea de culori diferite pentru obiectele 3D (în gradient sau selectate pentru fiecare față) ajută la evidențierea detaliilor geometrice, creând iluzia de profunzime și tridimensionalitate. Acest lucru îmbunătățește percepția vizuală și oferă un aspect mai realist obiectelor.


## 7. Ce reprezintă un gradient de culoare? Cum se obține acesta în OpenGL?

Un **gradient de culoare** este o tranziție lină între două sau mai multe culori. În OpenGL, gradientul se obține prin atribuirea de culori diferite fiecărui vertex al unei primitive (de exemplu, un triunghi). OpenGL va interpola culorile între vertexuri pentru a crea un efect de gradient.

## 10. Ce efect are utilizarea unei culori diferite pentru fiecare vertex atunci când desenăm o linie sau un triunghi în modul strip?

Când fiecare vertex are o culoare diferită într-un mod strip (linii sau triunghiuri), OpenGL va interpola culorile între vertexuri, creând un efect de gradient pe întreaga formă desenată.
