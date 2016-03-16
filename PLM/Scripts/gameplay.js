//Sounds from http://www.freesfx.co.uk/
var pictureAnswer = "default";
var count = 0;

function isGuessRight(answer, guess) {
    if (answer == guess) {
        document.getElementById("audioCorrect").play();
        return true;
    }
    else {
        document.getElementById("audioIncorrect").play();
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

function Correct() {
    count = (count + 1);
    document.getElementById("rightORwrong").innerText = count;
}