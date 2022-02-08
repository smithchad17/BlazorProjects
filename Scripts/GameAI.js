function chooseMove(move){

	myMove = move;
	var phase = document.getElementsByName('phase');

	if (phase[1].checked) {
		//choose from model
		var intMove = convertToOneHot(myMove);
		var xs = tf.tensor2d(intMove, [1, 3]);
		var logits = model.predict(xs).arraySync()[0];
		//get max
		opMove = moves[getMaxIndex(logits)];
	} else {
		//choose randomly 
		opMoveIdx = Math.floor((Math.random() * 3) + 0);
		opMove = moves[opMoveIdx];
	}

	return opMove;
}