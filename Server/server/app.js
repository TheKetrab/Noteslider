// ----- pkg -----
const http         = require('http');
const express      = require('express');
const trackManager = require('./trackManager.js');

var app = express();

var httpServer = http.createServer(app);
httpServer.listen(process.env.PORT || 5000);

app.set('view engine', 'ejs');
app.set('views', './views');
app.use(express.urlencoded( { extended:true } ));
app.use(express.static( 'public' ))

app.get("/", async (req, res) => {
    var info = await trackManager.readTracksInfo();
    res.send(info);
});

app.get("/:name", async (req, res) => {
    var track = await trackManager.readTrack(req.params['name']);
    res.setHeader('content-type', 'text/plain');
    res.send(track);
});

console.log("Server set up.");


