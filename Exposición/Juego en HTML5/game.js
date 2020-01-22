document.addEventListener('keydown', function(event) {
    if (event.keyCode == 32) {
        console.log("jump");

        if (level.dead == false)
            jump();
        else {
            level.speed = 9;
            level.dead = false;
            cloud.speed = 2;
            graphicGround.speed = level.speed + 16;
            rock.x = wid + 100;
            cloud.x = 400;
            level.points = 0;
        }
    }
}) 

var imgPlayer, imgCloud, imgObs, imgGround, imgSky, imgPucmm;

function loadImage() {
    // Carro
    imgPlayer = new Image();
    imgPlayer.src = 'img/car.png';
    // Nube
    imgCloud = new Image();
    imgCloud.src = 'img/cloud.png';
    // Roca
    imgObs = new Image();
    imgObs.src = 'img/rock.png';
    // Suelo
    imgGround = new Image();
    imgGround.src = 'img/ground.png';
    // Cielo
    imgSky = new Image();
    imgSky.src = 'img/Sky.png';
    // Logo de la pucmm
    imgPucmm = new Image();
    imgPucmm.src = 'img/pucmm.png';
}

var wid = 700;
var hei = 300;
var canvas, context;

function startGame() {
    canvas = document.getElementById('canvas');
    context = canvas.getContext('2d');
    loadImage();
}

function eraseCanvas() {
    /*
    Al cambiar las dimensiones del canvas,
    se borra todo automáticamente. Esta es la solución más
    óptima.
     */
    canvas.width = wid;
    canvas.height = hei;
}

var ground = 200;
var tcar = {  y: ground, vy: 0, gravity: 2, jump: 28, vymax: 9, jumping: false };
var level = {speed: 9, points: 0, dead: false};
var rock = {x: wid + 100, y: ground - 17};
var cloud = {x: 400, y: 50, speed: 2};
var graphicGround = {x: 0, y:ground, speed: level.speed + 16};


function drawPlayer() {
    // . ([0,0] (posicion))
    context.drawImage(imgPlayer, 0, 0, 509, 309, 100, tcar.y, 60, 50);
}

function drawRock() {
    context.drawImage(imgObs, 0, 0, 960, 634, rock.x, rock.y, 65, 75);
}

function rockLogic() {
    if (rock.x < -100) {
        rock.x = wid + 100;
        level.points++;
    } else {
        rock.x -= level.speed;
    }
}

function drawCloud() {
    context.drawImage(imgCloud, 0, 0, 662, 469, cloud.x, cloud.y, 82, 62);
}

function cloudLogic() {
    if (cloud.x < -100) {
        cloud.x = wid + 100;
    } else {
        cloud.x -= cloud.speed;
    }
}


function drawGround() {
    context.drawImage(imgSky, 0, 0, 2048, 1536, 0, 0, 800, 600);
    context.drawImage(imgPucmm, 0, 0, 392, 392, 10, 10, 40, 40);
    context.drawImage(imgGround, graphicGround.x, 0, 9006, 1511, 0, graphicGround.y, 2100, 120);
}

function groundLogic() {
    if (graphicGround.x > 2100) {
        graphicGround.x = 0;
    }
    else {
        graphicGround.x += graphicGround.speed;
    }
}


function jump() {
    if (tcar.y == ground) {
        tcar.jumping = true;
        tcar.vy = tcar.jump;
    }
}

function gravity() {
    if (tcar.jumping == true) {
        if (tcar.y - tcar.vy - tcar.gravity > ground) {
            tcar.jumping = false;
            tcar.vy = 0;
            tcar.y = ground;
        } else {
            tcar.vy -= tcar.gravity;
            tcar.y -= tcar.vy;          
        }
        
    }
}

function collision() {
    //rock.x, tcar.y
    if (rock.x >= 50 && rock.x <= 150) {
        if (tcar.y >= ground - 25) {
            level.dead = true;
            level.speed = 0;
            level.speed = 0;
            cloud.speed = 0;
            graphicGround.speed = 0;
        }
    }
}   

function pointsystem() {
    context.font = '30px impact';
    context.fillStyle = '#555555';
    context.fillText(`${level.points}`, 600, 50);

    if (level.dead == true) {
        context.font = '60px impact';
        context.fillText('GAME OVER', 240, 150);
    }
}

// Juego
var frame = 30;
setInterval(function(){
    principal();
}, 1000 / frame);

function principal() {
   eraseCanvas();   
   gravity();   
   collision();
   groundLogic();
   cloudLogic();
   rockLogic();
   drawGround();
   drawCloud();
   drawRock();
   drawPlayer();
   pointsystem();
}