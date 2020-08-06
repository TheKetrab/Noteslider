const fs  = require('fs');
const util = require('util');

const tracksDir = './public/tracks';

// returns json trackinfo
async function readTracksInfo() {

    let tracks = [];

    let dirEntries = await (util.promisify(fs.readdir))(tracksDir);
    dirEntries.forEach((file) => {
        let filename = file.substr(0,file.length-3);
        tracks.push(filename);
    });

    return JSON.stringify(tracks);
}



async function readTrack(trackName) {

    trackPath = `${tracksDir}/${trackName}.ns`;
    if (fs.existsSync(trackPath)) {
        let bytes = await (util.promisify(fs.readFile))(trackPath);
        return bytes;
    }

    return "";
}


module.exports = { readTracksInfo, readTrack }