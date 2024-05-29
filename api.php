<?php
require 'db.php';

header('Content-Type: application/json');

$method = $_SERVER['REQUEST_METHOD'];

if ($method == 'GET') {
    $stmt = $pdo->prepare("SELECT players.PlayerID, players.username, players.email, players.player_rank, match_details.MatchID, match_details.MapID FROM players LEFT JOIN match_details ON players.PlayerID = match_details.PlayerID");
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
    echo json_encode($result);
} elseif ($method == 'POST') {
    $data = json_decode(file_get_contents('php://input'), true);
    $username = $data['username'];
    $email = $data['email'];
    $birthday = $data['birthday'];
    $player_rank = $data['player_rank'];
    $password = password_hash($data['password'], PASSWORD_DEFAULT);
    $status = $data['status'];
    $is_admin = $data['is_admin'];
    $player_rank_numeric = $data['player_rank_numeric'];

    $stmt=$pdo->prepare("INSERT INTO players (username, email, birthday, player_rank,password, status, is_admin, player_rank_numeric) VALUES (:username, :email, :birthday, :player_rank, :password, :status, :is_admin, :player_rank_numeric)");
    $stmt->execute([
        ':username' => $username,
        ':email' => $email,
        ':birthday' => $birthday,
        ':player_rank' => $player_rank,
        ':password' => $password,
        ':status' => $status,
        ':is_admin' => $is_admin,
        ':player_rank_numeric' => $player_rank_numeric
    ]);

    $playerId = $pdo->lastInsertId();
    echo json_encode(['PlayerID' => $playerId, 'username' => $username, 'email' => $email, 'player_rank' => $player_rank ]);
}
?>