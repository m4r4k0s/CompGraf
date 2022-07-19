function main()
{
    let canvas = document.querySelector("#scene3d");
    let scene = canvas.getContext("2d");
    let Buton = document.querySelector("#Clear");


    let CurvePoints = [];

    let oldX; let oldY;
    let SHy=0; let SHx=0;
    let IsMove = false;
    let scale = 1;

    let mouseDownHandler = function (e)
    {
        if(e.which == 2)
        {
            let tmpX = e.offsetX*(1/scale)-SHx; let tmpY = e.offsetY*(1/scale)-SHy;
            console.log(tmpX, ' ', tmpY );
            CurvePoints.push(new Point({x:tmpX, y: tmpY}));
        }
        if (e.which == 1)
        {
            IsMove = true;
            oldX = e.offsetX; oldY = e.offsetY;
        }
        e.preventDefault();
    }
    let mouseUpHandler = function (e)
    {
        IsMove = false;
        e.preventDefault();
    }
    let mouseMoveHandler = function (e)
    {
        if (!IsMove)
        {
            return false;
        }
        if ( Math.abs(SHx - oldX + e.offsetX) <= canvas.clientWidth)
        {
            SHx -= oldX - e.offsetX;
            oldX = e.offsetX;
        }
        if (Math.abs(SHy - oldY + e.offsetY) <= canvas.clientHeight)
        {
            SHy -= oldY - e.offsetY;
            oldY = e.offsetY;
        }
        e.preventDefault();
    }
    let wheelHandler = function (e)
    {
        if (scale - e.deltaY/1000 >=0.5)
            scale -= e.deltaY/1000;
        e.preventDefault();
    }
    let ButonHandler = function (e)
    {
        CurvePoints = [];
        oldX=0; oldY=0;
        SHy=0; SHx=0;
        scale = 1;
        e.preventDefault();
    }

    canvas.addEventListener("mousedown", mouseDownHandler, false);
    canvas.addEventListener("mouseout", mouseUpHandler, false);
    canvas.addEventListener("mouseup", mouseUpHandler, false);
    canvas.addEventListener("mousemove", mouseMoveHandler, false);
    canvas.addEventListener("wheel", wheelHandler, false);
    Buton.addEventListener("mousedown", ButonHandler, false)


    let drawScene = function(time)
    {
        scene.clearRect(0,0,canvas.clientHeight,canvas.clientWidth);
        if(CurvePoints.length >= 2)
            drawByCurves(scene, Transformations(CurvePoints, scale, SHx, SHy));
        DrawLaoyut(scene, canvas.clientHeight, canvas.clientWidth, scale, SHx, SHy);
        DrawDots(scene, CurvePoints, scale, SHx, SHy);
        requestAnimationFrame(drawScene);
    }
    requestAnimationFrame(drawScene);
}

function drawByCurves(scene, points)
{
    scene.setLineDash([0,0]);
    scene.lineWidth = 2;
    scene.beginPath();
    scene.moveTo(points[0].X, points[0].Y);
    let dl = 0;
    for (let i = 0; i < points.length - 2; i++) {
        let dr = (points[i + 2].Y - points[i].Y) / 2;
        let a3 = dl + dr + 2 * (points[i].Y - points[i+1].Y);
        let a2 = points[i+1].Y - a3 - dl - points[i].Y;
        for (let t = 0; t <= 1; t+=.01) {
            let y = a3; y = y*t+a2; y = y*t+dl; y = y*t+points[i].Y;

            scene.lineTo(points[i].X + t * (points[i+1].X-points[i].X), y);
        }
        dl = dr;
    }
    scene.strokeStyle = 'blue';
    scene.stroke();
}

function DrawDots(scene, arr, scale, shx, shy)
{
    for (let i=0; i<arr.length; i++)
    {
        scene.strokeStyle = "rgb("+128+","+0+","+0+")";
        scene.fillStyle = "rgb("+128+","+0+","+0+")";
        if (arr[i].X != 0 && arr[i].Y !=0)
        {
            scene.beginPath();
            scene.arc((arr[i].X + shx) * scale, (arr[i].Y + shy) * scale, 3, 0, 2 * Math.PI);
            scene.fill();
        }
        scene.stroke();
    }
    for (let i = 0; i< arr.length-1; i++)
    {
        scene.setLineDash([5, 15]);
        scene.beginPath()
        scene.strokeStyle = "rgb("+128+","+0+","+0+")";
        scene.lineWidth = 1;
        scene.moveTo((arr[i].X+shx)*scale, (arr[i].Y+shy)*scale);
        scene.lineTo((arr[i+1].X+shx)*scale, (arr[i+1].Y+shy)*scale);
        scene.closePath();
        scene.stroke();
    }
}

function DrawLaoyut(scene, Y, X, scale, shx, shy)
{
    let startX = -X*2; let startY = -Y*2;
    for (let i=1; i<=100; i++)
    {
        scene.setLineDash([0,0]);
        scene.strokeStyle = "rgb("+128+","+128+","+128+")";
        scene.lineWidth = 1;
        scene.beginPath()
        scene.moveTo((startX+X/20*i+shx)*scale, startY);
        scene.lineTo((startX+X/20*i+shx)*scale, Y);
        scene.moveTo(startX, (startY+Y/20*i+shy)*scale);
        scene.lineTo(X, (startY+Y/20*i+shy)*scale);
        scene.closePath();
        scene.stroke();
    }
}

function Transformations(arr, scale, shx, shy)
{
    let res = [];
    for (let i =0; i< arr.length; i++)
    {
        let tmpX = (arr[i].X + shx) * scale; let tmpY = (arr[i].Y + shy) * scale;
        res.push(new Point({x:tmpX, y: tmpY}));
    }
    return res;
}

class Point
{
    constructor(param)
    {
        this._X = param.x;
        this._Y = param.y;
    }

    get X()
    {
        return this._X;
    }

    get Y()
    {
        return this._Y;
    }
}

main();