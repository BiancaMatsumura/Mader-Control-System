public class Cliente
{
    public int Id { get; set; }
    public DateTime? DataCadastro { get; set; } = DateTime.Now;  // Define como nullable para evitar problemas com valores nulos
    public string Nome { get; set; } = string.Empty;  // Obrigatório
    public string? NomeSocial { get; set; }
    public DateTime? DataNasc { get; set; }
    public string? CPF { get; set; }  
    public string? RG { get; set; }
    public string? CNPJ { get; set; }
    public string? IE { get; set; }
    public string? Celular01 { get; set; }
    public string? Celular02 { get; set; }
    public string? Email01 { get; set; }
    public string? Email02 { get; set; }
    public string? Endereco { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? CEP { get; set; }
    public string? Complemento { get; set; }
    public string? ReferenciasParaEntrega { get; set; }
    public string? ObservacoesGerais { get; set; }

    // Construtor padrão
    public Cliente() { }

    // Construtor completo
    public Cliente(string nome, DateTime? dataCadastro = null, string? nomeSocial = null, DateTime? dataNasc = null,
                   string? cpf = null, string? rg = null, string? cnpj = null, string? ie = null,
                   string? celular01 = null, string? celular02 = null, string? email01 = null, string? email02 = null,
                   string? endereco = null, string? bairro = null, string? cidade = null, string? estado = null,
                   string? cep = null, string? complemento = null, string? referenciasParaEntrega = null,
                   string? observacoesGerais = null)
    {
        Nome = nome;
        DataCadastro = dataCadastro ?? DateTime.Now;
        NomeSocial = nomeSocial;
        DataNasc = dataNasc;
        CPF = cpf;
        RG = rg;
        CNPJ = cnpj;
        IE = ie;
        Celular01 = celular01;
        Celular02 = celular02;
        Email01 = email01;
        Email02 = email02;
        Endereco = endereco;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Complemento = complemento;
        ReferenciasParaEntrega = referenciasParaEntrega;
        ObservacoesGerais = observacoesGerais;
    }

    // Método para exibir informações do cliente
    public override string ToString()
    {
        return $"ID: {Id}, Nome: {Nome}, Data de Cadastro: {DataCadastro?.ToString("yyyy-MM-dd")}, Endereço: {Endereco}, Cidade: {Cidade}";
    }

    // Método para validar campos obrigatórios
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Nome) &&
               !string.IsNullOrWhiteSpace(CPF) &&
               !string.IsNullOrWhiteSpace(Celular01) &&
               IsValidCPF(CPF) &&
               IsValidEmail(Email01);
    }

    // Método auxiliar para validar CPF (simplificado para exemplo)
    private bool IsValidCPF(string? cpf)
    {
        // Validação simplificada do CPF (apenas checagem de tamanho)
        return !string.IsNullOrEmpty(cpf) && cpf.Length == 11;
    }

    // Método auxiliar para validar e-mail
    private bool IsValidEmail(string? email)
    {
        return !string.IsNullOrEmpty(email) && email.Contains("@");
    }

    // Método para atualizar dados do cliente
    public void UpdateCliente(string? nomeSocial = null, DateTime? dataNasc = null, string? celular02 = null,
                              string? email01 = null, string? email02 = null, string? endereco = null,
                              string? bairro = null, string? cidade = null, string? estado = null, string? cep = null,
                              string? complemento = null, string? referenciasParaEntrega = null,
                              string? observacoesGerais = null)
    {
        NomeSocial = nomeSocial ?? NomeSocial;
        DataNasc = dataNasc ?? DataNasc;
        Celular02 = celular02 ?? Celular02;
        Email01 = email01 ?? Email01;
        Email02 = email02 ?? Email02;
        Endereco = endereco ?? Endereco;
        Bairro = bairro ?? Bairro;
        Cidade = cidade ?? Cidade;
        Estado = estado ?? Estado;
        CEP = cep ?? CEP;
        Complemento = complemento ?? Complemento;
        ReferenciasParaEntrega = referenciasParaEntrega ?? ReferenciasParaEntrega;
        ObservacoesGerais = observacoesGerais ?? ObservacoesGerais;
    }
}
