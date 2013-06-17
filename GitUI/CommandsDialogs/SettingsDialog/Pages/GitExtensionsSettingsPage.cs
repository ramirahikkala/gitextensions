﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GitCommands;
using GitCommands.Repository;

namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    public partial class GitExtensionsSettingsPage : SettingsPageBase
    {
        public GitExtensionsSettingsPage()
        {
            InitializeComponent();
            Text = "Git Extensions";
            Translate();
        }

        protected override void OnLoadSettings()
        {
            FillFromDropDown();

            chkCheckForUncommittedChangesInCheckoutBranch.Checked = Settings.CheckForUncommittedChangesInCheckoutBranch;
            chkStartWithRecentWorkingDir.Checked = Settings.StartWithRecentWorkingDir;
            chkPlaySpecialStartupSound.Checked = Settings.PlaySpecialStartupSound;
            chkWriteCommitMessageInCommitWindow.Checked = Settings.UseFormCommitMessage;
            chkUsePatienceDiffAlgorithm.Checked = Settings.UsePatienceDiffAlgorithm;
            RevisionGridQuickSearchTimeout.Value = Settings.RevisionGridQuickSearchTimeout;
            chkFollowRenamesInFileHistory.Checked = Settings.FollowRenamesInFileHistory;
            chkShowErrorsWhenStagingFiles.Checked = Settings.ShowErrorsWhenStagingFiles;
            chkStashUntrackedFiles.Checked = Settings.IncludeUntrackedFilesInAutoStash;
            chkShowCurrentChangesInRevisionGraph.Checked = Settings.RevisionGraphShowWorkingDirChanges;
            chkShowStashCountInBrowseWindow.Checked = Settings.ShowStashCount;
            chkShowGitStatusInToolbar.Checked = Settings.ShowGitStatusInBrowseToolbar;
            SmtpServer.Text = Settings.SmtpServer;
            SmtpServerPort.Text = Settings.SmtpPort.ToString();
            chkUseSSL.Checked = Settings.SmtpUseSsl;
            _NO_TRANSLATE_MaxCommits.Value = Settings.MaxRevisionGraphCommits;
            chkCloseProcessDialog.Checked = Settings.CloseProcessDialog;
            chkShowGitCommandLine.Checked = Settings.ShowGitCommandLine;
            chkUseFastChecks.Checked = Settings.UseFastChecks;
            cbDefaultCloneDestination.Text = Settings.DefaultCloneDestinationPath;
        }

        public override void SaveSettings()
        {
            Settings.CheckForUncommittedChangesInCheckoutBranch = chkCheckForUncommittedChangesInCheckoutBranch.Checked;
            Settings.StartWithRecentWorkingDir = chkStartWithRecentWorkingDir.Checked;
            Settings.PlaySpecialStartupSound = chkPlaySpecialStartupSound.Checked;
            Settings.UseFormCommitMessage = chkWriteCommitMessageInCommitWindow.Checked;
            Settings.UsePatienceDiffAlgorithm = chkUsePatienceDiffAlgorithm.Checked;
            Settings.ShowErrorsWhenStagingFiles = chkShowErrorsWhenStagingFiles.Checked;
            Settings.IncludeUntrackedFilesInAutoStash = chkStashUntrackedFiles.Checked;
            Settings.FollowRenamesInFileHistory = chkFollowRenamesInFileHistory.Checked;
            Settings.ShowGitStatusInBrowseToolbar = chkShowGitStatusInToolbar.Checked;
            Settings.SmtpServer = SmtpServer.Text;
            int port;
            if (int.TryParse(SmtpServerPort.Text, out port))
                Settings.SmtpPort = port;
            Settings.SmtpUseSsl = chkUseSSL.Checked;
            Settings.CloseProcessDialog = chkCloseProcessDialog.Checked;
            Settings.ShowGitCommandLine = chkShowGitCommandLine.Checked;
            Settings.UseFastChecks = chkUseFastChecks.Checked;
            Settings.MaxRevisionGraphCommits = (int)_NO_TRANSLATE_MaxCommits.Value;
            Settings.RevisionGridQuickSearchTimeout = (int)RevisionGridQuickSearchTimeout.Value;
            Settings.RevisionGraphShowWorkingDirChanges = chkShowCurrentChangesInRevisionGraph.Checked;
            Settings.ShowStashCount = chkShowStashCountInBrowseWindow.Checked;
            Settings.DefaultCloneDestinationPath = cbDefaultCloneDestination.Text;
        }

        private void chkUseSSL_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!chkUseSSL.Checked)
            {
                if (SmtpServerPort.Text == "587")
                    SmtpServerPort.Text = "465";
            }
            else
            {
                if (SmtpServerPort.Text == "465")
                    SmtpServerPort.Text = "587";
            }
        }

        private void FillFromDropDown()
        {
            System.ComponentModel.BindingList<Repository> repos = Repositories.RemoteRepositoryHistory.Repositories;
            if (cbDefaultCloneDestination.Items.Count != repos.Count)
            {
                cbDefaultCloneDestination.Items.Clear();
                foreach (Repository repo in repos)
                    cbDefaultCloneDestination.Items.Add(repo.Path);
            }
        }

        private void DefaultCloneDestinationBrowseClick(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog { SelectedPath = cbDefaultCloneDestination.Text })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    cbDefaultCloneDestination.Text = dialog.SelectedPath;
            }
        }
    }
}
