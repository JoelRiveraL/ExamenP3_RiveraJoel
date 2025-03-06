Feature: PedidosInsert

Ingreso de la informacion del Formulario Pedidos a la BDD

@tag1
Scenario: Insertar Nuevo Pedido
	Given Llenar los campos de Pedido.
	| ClienteID | Monto | Estado    |
	| 2         | 100   | Pendiente |
	When Registros ingresados en la DB.
	| ClienteID | Monto | Estado    |
	| 2         | 100   | Pendiente |
	Then Resultado ingreso en la base de datos.
	| ClienteID | Monto | Estado    |
	| 2         | 100   | Pendiente |
