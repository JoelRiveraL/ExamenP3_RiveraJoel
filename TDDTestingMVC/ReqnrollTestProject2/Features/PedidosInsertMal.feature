Feature: PedidosInsertMal

A short summary of the feature

@tag1
Scenario: Insertar Nuevo Pedido con datos inválidos
	Given Llenar mal los campos de Pedido.
	| ClienteID | Monto | Estado    |
	| 0         | 100   | Pendiente |
	When Intento ingresar el pedido malo en la DB.
	| ClienteID | Monto | Estado    |
	| 0         | 100   | Pendiente |
	Then El pedido mal no debe registrarse en la base de datos.
