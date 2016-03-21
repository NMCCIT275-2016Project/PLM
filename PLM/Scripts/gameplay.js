//Sounds from http://www.freesfx.co.uk/
var pictureAnswer = "default";
var count = 0;

function isGuessRight(answer, guess) {
    if (answer == guess) {
        document.getElementById("audioCorrect").play();
        reveal();
        showNext();
        return true;
    }
    else {
        document.getElementById("audioIncorrect").play();
        reveal();
        showNext();
        return false;
    }
}

function ButtonClick(guess) {
    pictureAnswer = document.getElementById("StoredAnswer").innerText;

    if (isGuessRight(pictureAnswer, guess)) {
        Correct();
    }
    else {

    }
}

function showNext() {
    document.getElementById("nextButton").style.display = "inline";
}

function reveal() {
    pictureAnswer = document.getElementById("StoredAnswer").innerText;

    $('.btn').each(function () {
        if (this.innerText == pictureAnswer) {
            this.style.backgroundColor = "green";
        } else {
            this.style.backgroundColor = "red";
        }
    });
}

function Correct() {
    count = (count + 100);
    document.getElementById("rightORwrong").innerText = count;
}