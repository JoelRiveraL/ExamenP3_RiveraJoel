Feature: EditarPedidos

A short summary of the feature

@tag1
Scenario: Editar Pedido Existente Mal.
	Given Un pedido existente dentro de la base de datos .
	| PedidoID | ClienteID | Monto | Estado   |
	| 1        | 2         | 150   | Aprobado |
	When Edito el pedido con nuevos datos.
	| PedidoID | ClienteID | Monto | Estado |
	| 1        | 0         | 0     | u      |
	Then El pedido no se debe actualizarse en la base de datos .
	| PedidoID | ClienteID | Monto | Estado |
	| 1        | 0         | 0     | u      |
