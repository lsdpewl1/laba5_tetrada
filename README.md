<html>
<head><h2>Вариант: Объявление целочисленной константы с инициализацией на языке Rust</h2></head>
<body>
    <p><b><i>Примеры допустимых строк:</b></i></p>
        <p>const abc:i32 = 123; </p>
        <p>const bcd:i32=123; </p>
        <p>const cde:i32 = -123; </p>
    <p><b><i>Разработанная грамматика:</b></i></p>    
        <p>const a:i32 = +-123;</p>
        <p>1) DEF -> ‘const’ CONST</p>
        <p>2) CONST -> ‘_’ ID</p>
        <p>3) ID ->letter IDREM</p>
        <p>4) IDREM -> letter IDREM</p>
        <p>5) IDREM -> ‘:’ TYPE</p>
        <p>6) TYPE -> ‘i32’ EQUAL</p>
        <p>7) EQUAL -> ‘=’ NUM</p>
        <p>8) NUM -> [+ | -] NUMBER</p>
        <p>9) NUMBER -> digit NUMBERREM</p>
        <p>10) NUMBERREM -> digit NUMBERREM</p>
        <p>11) NUMBERREM -> ;</p>
    <p><b><i>Классификация грамматики: </b></i>автоматная</p>       
        <p><b><i>Граф конечного автомата: </b></i></p> 
        <img src = "граф автомата последний2.jpg" style="width: 700px">
    <p><b><i>Тестовые примеры:</b></i></p>
    <img src = "ex1.png" style="width: 700px">
    <img src = "ex2.png" style="width: 700px">
    <img src = "ex3.png" style="width: 700px">
    <img src = "ex4.png" style="width: 700px">
    <img src = "ex5.png" style="width: 700px">
    <img src = "ex6.png" style="width: 700px">
</body>
</html>