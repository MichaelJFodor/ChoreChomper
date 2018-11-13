<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: access");
header("Access-Control-Allow-Methods: GET");
header("Access-Control-Allow-Credentials: true");
header("Content-Type: application/json; charset=UTF-8");
 
// include database and object files
include_once '../config/database.php';
include_once '../objects/chore.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// prepare chore object
$chore = new Chore($db);
 
// set ID property of record to read
$chore->c_id = isset($_GET['c_id']) ? $_GET['c_id'] : die();
 
// read the details of chore to be edited
$chore->readOne();
if($chore->c_name!=null){
    // create array
    $chore_arr = array(
        "choreId" =>  $chore->c_id,
        "choreName" => $chore->c_name,
        "deadlineTimestamp" => $chore->c_date
 
    );
    
    // set response code - 200 OK
    http_response_code(201);

    // make it json format
    echo json_encode($chore_arr);
}
 
else{
    // set response code - 404 Not found
    http_response_code(404);
 
    // tell the user product does not exist
    echo json_encode(array("message" => "Chore does not exist."));
}

?>