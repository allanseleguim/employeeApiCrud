
public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Ramal { get; set; }
    public string Email { get; set; }
    public decimal Salario { get; set; }
}


public class FuncionarioLog
{
    public int Id { get; set; }
    public string Acao { get; set; }
    public DateTime DataAcao { get; set; }
}
