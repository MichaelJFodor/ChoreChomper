<?php
	include 'connection.php';
	$conn = OpenCon();
	$cid = $_GET["ChoreId"];
	$time = $_GET["TimeStamp"];
	$completed_by = $_GET["CompletedBy"];
	$complete_chore = "UPDATE chorechomper.chores SET completed_by = '$completed_by', completed_date = '$time' WHERE (chore_id = '$cid')";
	if(mysqli_query($conn,$complete_chore))
	{
		echo 'success';
	}
	else
	{
		echo $cid;
		echo $time;
		echo $completed_by;
	}

	CloseCon($conn);
	?>