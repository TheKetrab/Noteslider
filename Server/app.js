// ----- pkg -----
const fs           = require('fs');
const http         = require('http');
const express      = require('express');

var app = express();

var httpServer = http.createServer(app);
httpServer.listen(process.env.PORT || 5000);

app.set('view engine', 'ejs');
app.set('views', './views');
app.use(express.urlencoded( { extended:true } ));
app.use(express.static( 'public' ))

app.get("/", (req, res) => {
    res.render('index' );
});

console.log("Server set up.");


