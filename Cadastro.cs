using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Cadastro
{
    public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public List<Ambiente> Ambientes { get; set; } = new List<Ambiente>();

    public void AdicionarUsuario(Usuario usuario)
    {
        if (usuario != null)
        {
            Usuarios.Add(usuario);
        }
        else
        {
            Console.WriteLine("Não é possível adicionar um usuário nulo.");
        }
    }

    public bool RemoverUsuario(Usuario usuario)
    {
        if (usuario != null)
        {
            return Usuarios.Remove(usuario);
        }

        Console.WriteLine("Não é possível remover um usuário nulo.");
        return false;
    }

    public Usuario PesquisarUsuario(Usuario usuario)
    {
        return Usuarios.Find(u => u.Equals(usuario));
    }

    public void AdicionarAmbiente(Ambiente ambiente)
    {
        if (ambiente != null)
        {
            Ambientes.Add(ambiente);
        }
        else
        {
            Console.WriteLine("Não é possível adicionar um ambiente nulo.");
        }
    }

    public bool RemoverAmbiente(Ambiente ambiente)
    {
        if (ambiente != null)
        {
            return Ambientes.Remove(ambiente);
        }

        Console.WriteLine("Não é possível remover um ambiente nulo.");
        return false;
    }

    public Ambiente PesquisarAmbiente(Ambiente ambiente)
    {
        return Ambientes.Find(a => a.Equals(ambiente));
    }

    public void Upload()
    {
        try
        {
            using (FileStream fs = new FileStream("cadastro.bin", FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }

            Console.WriteLine("Upload concluído com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao realizar o upload: {ex.Message}");
        }
    }

    public void Download()
    {
        try
        {
            using (FileStream fs = new FileStream("cadastro.bin", FileMode.Open))
            {
                IFormatter formatter = new BinaryFormatter();
                Cadastro cadastro = (Cadastro)formatter.Deserialize(fs);
                this.Usuarios = cadastro.Usuarios;
                this.Ambientes = cadastro.Ambientes;
            }

            Console.WriteLine("Download concluído com sucesso.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Arquivo de cadastro não encontrado para download.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao realizar o download: {ex.Message}");
        }
    }
}