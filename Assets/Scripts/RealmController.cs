using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class RealmController : MonoBehaviour
{
    private Realm _realm;

    public static RealmController Instance;

    public string RealmAppId = "amaze-kwzxn";

    private App realmApp;
    private User _realmUser;

    public void LoginClic()
    {
        PreLogin();
    }

    async void PreLogin()
    {
        string loginResponse = await Login("hi@hi", "hi");
    }

    public async Task<string> Login(string email, string password)
    {
        if (email != "" && password != "")
        {
            realmApp = App.Create(new AppConfiguration(RealmAppId)
            {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            try
            {
                _realmUser = await realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                _realm = await Realm.GetInstanceAsync(new PartitionSyncConfiguration(email, _realmUser));
            }
            catch (ClientResetException clientResetEx)
            {
                if (_realm != null)
                {
                    _realm.Dispose();
                }
                clientResetEx.InitiateClientReset();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }
        return "All fields are required!";
    }

    private void OnDisable()
    {
        _realm.Dispose();
    }

    public GameModel GetPlayerProfile()
    {
        GameModel _playerProfile = _realm.Find<GameModel>(_realmUser.Id);
        if (_playerProfile == null)
        {
            _realm.Write(() =>
            {
                _playerProfile = _realm.Add(new GameModel(_realmUser.Id));
            });
        }
        return _playerProfile;
    }

    // how to use
    public void IncreaseScore()
    {
        GameModel _playerProfile = GetPlayerProfile();
        if (_playerProfile != null)
        {
            _realm.Write(() =>
            {
                _playerProfile.HighScore = 10;
            });
        }
    }
}
